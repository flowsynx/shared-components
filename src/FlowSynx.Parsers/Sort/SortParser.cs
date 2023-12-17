using Microsoft.Extensions.Logging;
using EnsureThat;
using FlowSynx.Parsers.Exceptions;

namespace FlowSynx.Parsers.Sort;

internal class SortParser : ISortParser
{
    private readonly ILogger<SortParser> _logger;

    public SortParser(ILogger<SortParser> logger)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        _logger = logger;
    }

    public List<SortInfo> Parse(string sortStatement, IEnumerable<string> properties)
    {
        if (string.IsNullOrEmpty(sortStatement))
            sortStatement = "name asc";

        return ParseSortWithSuffix(sortStatement, properties);
    }

    protected List<SortInfo> ParseSortWithSuffix(string sortStatement, IEnumerable<string> properties)
    {
        var items = sortStatement.Split(',').Select(p => p.Trim());
        return items.Select(x => ParseSortTerms(x, properties)).ToList();
    }

    private SortInfo ParseSortTerms(string item, IEnumerable<string> properties)
    {
        var pair = item.Split(' ').Select(p => p.Trim()).ToList();
        if (pair.Count > 2)
        {
            _logger.LogError($"Invalid OrderBy string '{item}'. Order By Format: Property, Property2 ASC, Property2 DESC");
            throw new SortParserException(string.Format(Resources.SortParserInvalidSortingTerm, item));
        }

        var prop = pair[0];

        if (string.IsNullOrEmpty(prop))
            throw new SortParserException(Resources.SortParserInvalidProperty);

        var name = NormalizePropertyName(prop, properties);

        var direction = SortDirection.Ascending;
        if (pair.Count == 2)
            direction = NormalizeSortDirection(pair[1], name);

        return new SortInfo { Name = name, Direction = direction };
    }

    private string NormalizePropertyName(string propertyName, IEnumerable<string> properties)
    {
        if (properties.Contains(propertyName, StringComparer.OrdinalIgnoreCase)) return propertyName;

        _logger.LogError($"Invalid Property. '{propertyName}' is not valid.");
        throw new SortParserException(string.Format(Resources.SortParserInvalidPropertyName, propertyName));

    }

    private SortDirection NormalizeSortDirection(string sortDirection, string property)
    {
        string[] terms = { "Asc", "Desc" };

        if (terms.Contains(sortDirection, StringComparer.OrdinalIgnoreCase))
            return string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase) ? SortDirection.Descending : SortDirection.Ascending;

        _logger.LogWarning($"Sort direction '{sortDirection}' for '{property}' is not valid.");
        throw new SortParserException(string.Format(Resources.SortParserInvalidSortDirection, sortDirection, property));
    }

    public void Dispose() { }
}