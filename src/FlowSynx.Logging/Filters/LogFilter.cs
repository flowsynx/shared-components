using System.Text.RegularExpressions;
using EnsureThat;
using FlowSynx.Abstractions.Attributes;
using FlowSynx.Logging.Extensions;
using FlowSynx.Logging.Options;
using FlowSynx.Parsers.Date;
using FlowSynx.Parsers.Extensions;
using FlowSynx.Parsers.Percent;
using FlowSynx.Parsers.Sort;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Logging.Filters;

public class LogFilter : ILogFilter
{
    private readonly ILogger<LogFilter> _logger;
    private readonly IDateParser _dateParser;
    private readonly ISortParser _sortParser;
    private readonly IPercentParser _percentParser;

    public LogFilter(ILogger<LogFilter> logger, IDateParser dateParser,
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

    public IEnumerable<LogMessage> FilterLogsList(IEnumerable<LogMessage> logs,
        LogSearchOptions searchOptions, LogListOptions listOptions)
    {
        var logsList = logs.ToList();
        if (!logsList.Any())
            return logsList;

        var predicate = PredicateBuilder.True<LogMessage>();

        if (!string.IsNullOrEmpty(searchOptions.Include))
        {
            var myRegex = new Regex(searchOptions.Include, searchOptions.CaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);
            predicate = predicate.And(d => myRegex.IsMatch(d.Message.ToString()));
        }
        if (!string.IsNullOrEmpty(searchOptions.Exclude) && string.IsNullOrEmpty(searchOptions.Include))
        {
            var myRegex = new Regex(searchOptions.Exclude, searchOptions.CaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);
            predicate = predicate.And(d => !myRegex.IsMatch(d.Message.ToString()));
        }

        if (!string.IsNullOrEmpty(searchOptions.MinimumAge))
        {
            var parsedDateTime = _dateParser.Parse(searchOptions.MinimumAge);
            predicate = predicate.And(p => p.TimeStamp >= parsedDateTime);
        }

        if (!string.IsNullOrEmpty(searchOptions.MaximumAge))
        {
            var parsedDateTime = _dateParser.Parse(searchOptions.MaximumAge);
            predicate = predicate.And(p => p.TimeStamp <= parsedDateTime);
        }

        if (!string.IsNullOrEmpty(searchOptions.Level))
        {
            var level = searchOptions.Level.ToStandardLogLevel();
            predicate = predicate.And(p => p.Level == level);
        }

        var result = logsList.Where(predicate.Compile());

        if (!string.IsNullOrEmpty(listOptions.Sorting))
        {
            var parsedSort = _sortParser.Parse(listOptions.Sorting, ObjectPropertiesList<LogMessage>());
            result = result.Sorting(parsedSort);
        }

        var maxResult = _percentParser.Parse(listOptions.MaxResult, logsList.Count());
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