﻿using FlowSynx.Abstractions.Attributes;

namespace FlowSynx.Configuration;

public class ConfigurationItem: IEquatable<ConfigurationItem>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? ModifiedTime { get; set; }

    public Dictionary<string, string?>? Specifications { get; set; }

    public override string ToString()
    {
        return $"{Type}:{Name}";
    }

    public bool Equals(ConfigurationItem? other)
    {
        if (other == null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (Id != other.Id) return false;
        if (Name != other.Name) return false;

        return true;
    }
}