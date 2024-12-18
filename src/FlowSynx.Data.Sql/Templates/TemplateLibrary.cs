﻿namespace FlowSynx.Data.Sql.Templates;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public static class TemplateLibrary
{
    public static Template CreateTable
    {
        get
        {
            const string sql = "{{START}}CREATE TABLE {{TABLE}} {{CREATETABLEFIELDS}}{{END}}";
            return new Template(sql);
        }
    }

    public static Template Select
    {
        get
        {
            const string sql = "{{START}}SELECT {{FIELDS}} FROM {{TABLE}}{{JOINS}}{{FILTERS}}{{GROUPBY}}{{ORDERBY}}{{FETCHES}}{{END}}";
            return new Template(sql);
        }
    }

    public static Template Insert
    {
        get
        {
            const string sql = "{{START}}INSERT INTO {{TABLE}} ({{FIELDS}}) VALUES({{VALUES}}){{END}}";
            return new Template(sql);
        }
    }

    public static Template BulkInsert
    {
        get
        {
            const string sql = "{{START}}INSERT INTO {{TABLE}} ({{FIELDS}}) VALUES {{VALUES}}{{END}}";
            return new Template(sql);
        }
    }

    public static Template Delete
    {
        get
        {
            const string sql = "{{START}}DELETE FROM {{TABLE}}{{FILTERS}}{{END}}";
            return new Template(sql);
        }
    }

    public static Template DropTable
    {
        get
        {
            const string sql = "{{START}}DROP TABLE {{TABLE}}{{END}}";
            return new Template(sql);
        }
    }

    public static Template ExistTable
    {
        get
        {
            const string sql = "{{START}}SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = {{TABLE}}{{END}}";
            return new Template(sql);
        }
    }

    public static Template TableFields
    {
        get
        {
            const string sql = "{{START}}SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = {{TABLE}}{{END}}";
            return new Template(sql);
        }
    }
}