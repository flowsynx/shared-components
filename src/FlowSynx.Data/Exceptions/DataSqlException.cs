﻿using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Data.Exceptions;

public class DataSqlException : FlowSynxException
{
    public DataSqlException(string message) : base(message) { }
    public DataSqlException(string message, Exception inner) : base(message, inner) { }
}