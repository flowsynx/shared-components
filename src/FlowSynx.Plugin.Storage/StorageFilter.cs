using EnsureThat;
using FlowSynx.Abstractions.Attributes;
using FlowSynx.Plugin.Abstractions;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using FlowSynx.Parsers.Date;
using FlowSynx.Parsers.Extensions;
using FlowSynx.Parsers.Percent;
using FlowSynx.Parsers.Size;
using FlowSynx.Parsers.Sort;
using FlowSynx.Plugin.Abstractions.Extensions;
using FlowSynx.Plugin.Storage.Options;

namespace FlowSynx.Plugin.Storage;

public class StorageFilter : IStorageFilter
{
    private readonly ILogger<StorageFilter> _logger;
    private readonly IDateParser _dateParser;
    private readonly ISizeParser _sizeParser;
    private readonly ISortParser _sortParser;
    private readonly IPercentParser _percentParser;

    public StorageFilter(ILogger<StorageFilter> logger, IDateParser dateParser,
        ISizeParser sizeParser, ISortParser sortParser, IPercentParser percentParser)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(dateParser, nameof(dateParser));
        EnsureArg.IsNotNull(sizeParser, nameof(sizeParser));
        EnsureArg.IsNotNull(sortParser, nameof(sortParser));
        EnsureArg.IsNotNull(percentParser, nameof(percentParser));
        _logger = logger;
        _dateParser = dateParser;
        _sizeParser = sizeParser;
        _sortParser = sortParser;
        _percentParser = percentParser;
    }

    public IEnumerable<StorageEntity> Filter(IEnumerable<StorageEntity> entities, PluginOptions? options)
    {
        var storageEntities = entities.ToList();
        if (!storageEntities.Any())
            return storageEntities;

        var predicate = PredicateBuilder.True<StorageEntity>();
        var listFilters = options.ToObject<ListOptions>();

        predicate = listFilters.Kind?.ToLower() switch
        {
            "file" => predicate.And(p => p.Kind == StorageEntityItemKind.File),
            "directory" => predicate.And(p => p.Kind == StorageEntityItemKind.Directory),
            _ => predicate
        };

        if (!string.IsNullOrEmpty(listFilters.Include))
        {
            var myRegex = new Regex(listFilters.Include, listFilters.CaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);
            predicate = predicate.And(d => myRegex.IsMatch(d.Name));
        }
        if (!string.IsNullOrEmpty(listFilters.Exclude) && string.IsNullOrEmpty(listFilters.Include))
        {
            var myRegex = new Regex(listFilters.Exclude, listFilters.CaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);
            predicate = predicate.And(d => !myRegex.IsMatch(d.Name));
        }
        if (!string.IsNullOrEmpty(listFilters.MinAge))
        {
            var parsedDateTime = _dateParser.Parse(listFilters.MinAge);
            predicate = predicate.And(p => p.CreatedTime >= parsedDateTime);
        }
        if (!string.IsNullOrEmpty(listFilters.MaxAge))
        {
            var parsedDateTime = _dateParser.Parse(listFilters.MaxAge);
            predicate = predicate.And(p => p.CreatedTime <= parsedDateTime);
        }
        if (!string.IsNullOrEmpty(listFilters.MinSize))
        {
            var parsedSize = _sizeParser.Parse(listFilters.MinSize);
            predicate = predicate.And(p => p.Size >= parsedSize && p.Kind == StorageEntityItemKind.File);
        }
        if (!string.IsNullOrEmpty(listFilters.MaxSize))
        {
            var parsedSize = _sizeParser.Parse(listFilters.MaxSize);
            predicate = predicate.And(p => p.Size <= parsedSize && p.Kind == StorageEntityItemKind.File);
        }

        var result = storageEntities.Where(predicate.Compile());

        if (!string.IsNullOrEmpty(listFilters.Sorting))
        {
            var parsedSort = _sortParser.Parse(listFilters.Sorting, ObjectPropertiesList<StorageEntity>());
            result = result.Sorting(parsedSort);
        }

        var maxResult = _percentParser.Parse(listFilters.Limit, storageEntities.Count());
        if (maxResult > 0)
            result = result.Take(maxResult);

        return result;
    }

    protected IEnumerable<string> ObjectPropertiesList<T>()
    {
        try
        {
            var properties = typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(SortMemberAttribute))).ToList();
            if (!properties.Any())
                properties = typeof(T).GetProperties().ToList();

            return properties.Select(x => x.Name);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new List<string>();
        }
    }
}