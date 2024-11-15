﻿using System.Text.RegularExpressions;

namespace FlowSynx.Data.SqlQuery.Templates;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class Template
{
    private const string EscStart = "{{";
    private const string EscEnd = "}}";

    public Template(string pattern)
    {
        Pattern = pattern;
        Snippets = new List<Snippet>();
    }

    public string Pattern { get; set; }
    public List<Snippet> Snippets { get; set; }

    public Template Append(params Snippet[] snippets)
    {
        foreach (var snippet in snippets)
            Snippets.Add(snippet);
        return this;
    }

    public Template Append(string name, string code, string prefix = "", string postfix = "")
    {
        var snippet = new Snippet(name, code, prefix, postfix);
        Append(snippet);
        return this;
    }

    public string GetSql(Format format)
    {
        var pattern = Pattern;

        Append(SnippetLibrary.End(format.EndOfStatement.ToString()));

        foreach (var snippet in Snippets)
        {
            var text = EscStart + snippet.Name + EscEnd;
            if (pattern.Contains(text))
            {
                pattern = pattern.Replace(text, snippet.Prefix + snippet.Code + snippet.Postfix);
            }
        }

        pattern = Regex.Replace(pattern, EscStart + "([A-Za-z0-9_]+)" + EscEnd, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        return pattern;
    }
}