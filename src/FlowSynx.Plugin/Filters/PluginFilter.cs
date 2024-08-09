using System.Text.RegularExpressions;
using EnsureThat;
using FlowSynx.Abstractions.Attributes;
using FlowSynx.Parsers.Extensions;
using FlowSynx.Parsers.Percent;
using FlowSynx.Parsers.Sort;
using FlowSynx.Plugin.Abstractions;
using FlowSynx.Plugin.Options;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Plugin.Filters;

public class PluginFilter : IPluginFilter
{
    private readonly ILogger<PluginFilter> _logger;
    private readonly ISortParser _sortParser;
    private readonly IPercentParser _percentParser;

    public PluginFilter(ILogger<PluginFilter> logger, ISortParser sortParser, IPercentParser percentParser)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(sortParser, nameof(sortParser));
        EnsureArg.IsNotNull(percentParser, nameof(percentParser));
        _logger = logger;
        _sortParser = sortParser;
        _percentParser = percentParser;
    }

    public IEnumerable<IPlugin> FilterPluginsList(IEnumerable<IPlugin> plugins, 
        PluginSearchOptions searchOptions, PluginListOptions listOptions)
    {
        var pluginsList = plugins.ToList();
        if (!pluginsList.Any())
            return pluginsList;

        var predicate = PredicateBuilder.True<IPlugin>();

        if (!string.IsNullOrEmpty(searchOptions.Include))
        {
            var myRegex = new Regex(searchOptions.Include, searchOptions.CaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);
            predicate = predicate.And(d => myRegex.IsMatch(d.Type.ToString()));
        }
        if (!string.IsNullOrEmpty(searchOptions.Exclude) && string.IsNullOrEmpty(searchOptions.Include))
        {
            var myRegex = new Regex(searchOptions.Exclude, searchOptions.CaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);
            predicate = predicate.And(d => !myRegex.IsMatch(d.Type.ToString()));
        }

        var result = pluginsList.Where(predicate.Compile());

        if (!string.IsNullOrEmpty(listOptions.Sorting))
        {
            var parsedSort = _sortParser.Parse(listOptions.Sorting, ObjectPropertiesList<IPlugin>());
            result = result.Sorting(parsedSort);
        }

        var maxResult = _percentParser.Parse(listOptions.MaxResult, pluginsList.Count());
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