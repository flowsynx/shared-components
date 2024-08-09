using System.Text.RegularExpressions;
using EnsureThat;
using FlowSynx.Abstractions.Attributes;
using FlowSynx.Configuration.Options;
using FlowSynx.Parsers.Date;
using FlowSynx.Parsers.Extensions;
using FlowSynx.Parsers.Percent;
using FlowSynx.Parsers.Sort;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Configuration.Filters;

public class ConfigurationFilter : IConfigurationFilter
{
    private readonly ILogger<ConfigurationFilter> _logger;
    private readonly IDateParser _dateParser;
    private readonly ISortParser _sortParser;
    private readonly IPercentParser _percentParser;

    public ConfigurationFilter(ILogger<ConfigurationFilter> logger, IDateParser dateParser,
        ISortParser sortParser, IPercentParser percentParser)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(dateParser, nameof(dateParser));
        EnsureArg.IsNotNull(sortParser, nameof(sortParser));
        EnsureArg.IsNotNull(percentParser, nameof(percentParser));
        _logger = logger;
        _dateParser = dateParser;
        _sortParser = sortParser;
        _percentParser = percentParser;
    }

    public IEnumerable<ConfigurationItem> FilterConfigurationList(IEnumerable<ConfigurationItem> entities, ConfigurationSearchOptions searchOptions,
        ConfigurationListOptions listOptions)
    {
        var storageEntities = entities.ToList();
        if (!storageEntities.Any())
            return storageEntities;

        var predicate = PredicateBuilder.True<ConfigurationItem>();

        if (!string.IsNullOrEmpty(searchOptions.Include))
        {
            var myRegex = new Regex(searchOptions.Include, searchOptions.CaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);
            predicate = predicate.And(d => myRegex.IsMatch(d.Name));
        }
        if (!string.IsNullOrEmpty(searchOptions.Exclude) && string.IsNullOrEmpty(searchOptions.Include))
        {
            var myRegex = new Regex(searchOptions.Exclude, searchOptions.CaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);
            predicate = predicate.And(d => !myRegex.IsMatch(d.Name));
        }
        if (!string.IsNullOrEmpty(searchOptions.MinimumAge))
        {
            var parsedDateTime = _dateParser.Parse(searchOptions.MinimumAge);
            predicate = predicate.And(p => p.CreatedTime >= parsedDateTime);
        }
        if (!string.IsNullOrEmpty(searchOptions.MaximumAge))
        {
            var parsedDateTime = _dateParser.Parse(searchOptions.MaximumAge);
            predicate = predicate.And(p => p.CreatedTime <= parsedDateTime);
        }

        var result = storageEntities.Where(predicate.Compile());

        if (!string.IsNullOrEmpty(listOptions.Sorting))
        {
            var parsedSort = _sortParser.Parse(listOptions.Sorting, ObjectPropertiesList<ConfigurationItem>());
            result = result.Sorting(parsedSort);
        }

        var maxResult = _percentParser.Parse(listOptions.MaxResult, storageEntities.Count());
        if (maxResult > 0)
            result = result.Take(maxResult);

        return result;
    }

    protected IEnumerable<string> ObjectPropertiesList<T>()
    {
        try
        {
            var properties = typeof(T)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(SortMemberAttribute)))
                .ToList();

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