using EnsureThat;
using FlowSynx.Parsers.Exceptions;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Parsers.Percent;

internal class PercentParser : IPercentParser
{
    private readonly ILogger<PercentParser> _logger;

    public PercentParser(ILogger<PercentParser> logger)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        _logger = logger;
    }

    public int Parse(string? value, long total)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(value) || total <= 0)
                return 0;

            double? result = 0;
            if (value.Contains("%"))
                result = (double.Parse(value.Replace("%", "")) / 100) * total;
            else
            {
                result = double.Parse(value);
            }

            return result is > 0 ? (int)Math.Ceiling(result.Value) : 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new PercentParserException(ex.Message);
        }
    }

    public void Dispose() { }
}