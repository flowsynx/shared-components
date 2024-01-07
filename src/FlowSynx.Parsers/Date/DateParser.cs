using EnsureThat;
using FlowSynx.Environment;
using FlowSynx.Parsers.Exceptions;
using FlowSynx.Parsers.Extensions;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Parsers.Date;

public class DateParser : IDateParser
{
    public double Years { get; private set; } = 0.0;
    public double Months { get; private set; } = 0.0;
    public double Weeks { get; private set; } = 0.0;
    public double Days { get; private set; } = 0.0;
    public double Hours { get; private set; } = 0.0;
    public double Minutes { get; private set; } = 0.0;
    public double Seconds { get; private set; } = 0.0;
    public double Milliseconds { get; private set; } = 0.0;
    private readonly ILogger<DateParser> _logger;
    private readonly ISystemClock _systemClock;

    public DateParser(ILogger<DateParser> logger, ISystemClock systemClock)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(systemClock, nameof(systemClock));
        _logger = logger;
        _systemClock = systemClock;
    }

    public DateTime Parse(string dateTime)
    {
        var isDateTime = DateTime.TryParse(dateTime, out var dateTimeResult);
        if (isDateTime)
            return dateTimeResult;

        var isDateTimeDouble = double.TryParse(dateTime, out var doubleResult);
        if (isDateTimeDouble)
            dateTime += 's';

        return ParseDateTimeWithSuffix(dateTime);
    }

    protected DateTime ParseDateTimeWithSuffix(string dateTime)
    {
        if (!HasValidSuffix(dateTime))
        {
            _logger.LogError($"The given datetime '{dateTime}' is not valid!");
            throw new DateParserException(Resources.DateParserInvalidInput);
        }

        var lastPos = 0;
        var phase = DateTimeSpanPhase.Years;

        while (phase != DateTimeSpanPhase.Done)
        {
            switch (phase)
            {
                case DateTimeSpanPhase.Years:
                    Years = ExtractValue(dateTime, "y", ref lastPos);
                    phase = DateTimeSpanPhase.Months;
                    break;
                case DateTimeSpanPhase.Months:
                    Months = ExtractValue(dateTime, "M", ref lastPos);
                    phase = DateTimeSpanPhase.Weeks;
                    break;
                case DateTimeSpanPhase.Weeks:
                    Weeks = ExtractValue(dateTime, "w", ref lastPos);
                    phase = DateTimeSpanPhase.Days;
                    break;
                case DateTimeSpanPhase.Days:
                    Days = ExtractValue(dateTime, "d", ref lastPos);
                    phase = DateTimeSpanPhase.Hours;
                    break;
                case DateTimeSpanPhase.Hours:
                    Hours = ExtractValue(dateTime, "h", ref lastPos);
                    phase = DateTimeSpanPhase.Minutes;
                    break;
                case DateTimeSpanPhase.Minutes:
                    if (dateTime.IndexOf("ms", StringComparison.Ordinal) < 0)
                        Minutes = ExtractValue(dateTime, "m", ref lastPos);

                    phase = DateTimeSpanPhase.Seconds;
                    break;
                case DateTimeSpanPhase.Seconds:
                    if (dateTime.IndexOf("ms", StringComparison.Ordinal) < 0)
                        Seconds = ExtractValue(dateTime, "s", ref lastPos);

                    phase = DateTimeSpanPhase.Milliseconds;
                    break;
                case DateTimeSpanPhase.Milliseconds:
                    Milliseconds = ExtractValue(dateTime, "ms", ref lastPos);
                    phase = DateTimeSpanPhase.Done;
                    break;
            }
        }

        return _systemClock.NowUtc
            .AddYears(Years).AddMonths(Months)
            .AddWeeks(Weeks).AddDays(Days)
            .AddHours(Hours).AddMinutes(Minutes)
            .AddSeconds(Seconds).AddMilliseconds(Milliseconds);
    }

    protected bool HasValidSuffix(string date)
    {
        var letters = new string(date.Where(char.IsLetter).ToArray());
        var validSymbols = new List<char>() { 'y', 'M', 'w', 'd', 'h', 'm', 's' };
        return letters.All(symbol => validSymbols.Contains(symbol));
    }

    protected double ExtractValue(string dateTime, string key, ref int position)
    {
        try
        {
            var charLocation = dateTime.IndexOf(key, StringComparison.Ordinal);
            if (charLocation < 0)
                return 0;
            
            var extractedValue = dateTime.Substring(position, charLocation - position);
            var validValue = double.TryParse(extractedValue, out var val);
            position = charLocation + 1;
            return validValue ? val : 0.0;
        }
        catch (Exception)
        {
            _logger.LogWarning($"Error in validating and parsing '{dateTime}' string.");
            throw new DateParserException(string.Format(Resources.ErrorInValidatingAndParsingDateTimeString, dateTime));
        }
    }

    public void Dispose() { }
}