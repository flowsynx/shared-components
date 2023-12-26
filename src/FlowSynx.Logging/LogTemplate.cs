﻿using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text;

namespace FlowSynx.Logging;

internal static class LogTemplate
{
    internal static string Format(LogMessage logMessage, string template)
    {
        var sbResult = new StringBuilder(template.Length);
        var sbCurrentTerm = new StringBuilder();
        var formatChars = template.ToCharArray();
        var inTerm = false;

        for (var i = 0; i < template.Length; i++)
        {
            if (formatChars[i] == '{')
                inTerm = true;
            else if (formatChars[i] == '}')
            {
                string? valueToAppend;
                if (sbCurrentTerm.ToString() == "NewLine")
                {
                    valueToAppend = Environment.NewLine;
                }
                else
                {
                    var propertyInfo = logMessage.GetType().GetProperty(sbCurrentTerm.ToString(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo == null)
                        throw new Exception($"The property '{sbCurrentTerm.ToString()}' is not valid!");

                    var propertyValue = propertyInfo.GetValue(logMessage);

                    if (string.Equals(propertyInfo.Name, "level", StringComparison.OrdinalIgnoreCase))
                        propertyValue = GetShortLogLevel((LogLevel) propertyValue!);

                    valueToAppend = propertyValue is null ? string.Empty : propertyValue.ToString();
                }

                sbResult.Append(valueToAppend);
                sbCurrentTerm.Clear();
                inTerm = false;
            }
            else if (inTerm)
            {
                sbCurrentTerm.Append(formatChars[i]);
            }
            else
                sbResult.Append(formatChars[i]);
        }
        return sbResult.ToString();
    }

    internal static string GetShortLogLevel(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "TRCE",
            LogLevel.Debug => "DBUG",
            LogLevel.Information => "INFO",
            LogLevel.Warning => "WARN",
            LogLevel.Error => "FAIL",
            LogLevel.Critical => "CRIT",
            _ => logLevel.ToString().ToUpper()
        };
    }
}