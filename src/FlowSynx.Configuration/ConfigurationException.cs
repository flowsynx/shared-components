﻿using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Configuration;

public class ConfigurationException : FlowSynxException
{
    public ConfigurationException(string message) : base(message) { }
    public ConfigurationException(string message, Exception inner) : base(message, inner) { }
}