﻿using System.Text.RegularExpressions;

namespace FlowSynx.Data.SqlQuery.Templates;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class Snippet
{

    private string _name;

    public string Code { get; set; }

    public string Prefix { get; set; }

    public string Postfix { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            if (!IsValid(value))
                throw new Exception($"{value} {Name}");

            _name = value;
        }
    }

    public Snippet(string name, string code, string prefix = "", string postfix = "")
    {
        Name = name;
        Code = code;
        Prefix = prefix;
        Postfix = postfix;
    }

    private bool IsValid(string name)
    {
        return Regex.IsMatch(name, "^([A-Za-z0-9_]+)$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    }

}