namespace FlowSynx.IO.Cache;

public class MultiKeyCache<TPrimaryKey, TSecondaryKey, TValue> : 
    IMultiKeyCache<TPrimaryKey, TSecondaryKey, TValue> 
    where TPrimaryKey : notnull where TSecondaryKey : notnull
{
    private readonly Dictionary<TPrimaryKey, Dictionary<TSecondaryKey, TValue>> _entries;

    public MultiKeyCache()
    {
        _entries = new Dictionary<TPrimaryKey, Dictionary<TSecondaryKey, TValue>>();
    }

    public TValue? Get(TPrimaryKey primaryKey, TSecondaryKey secondaryKey)
    {
        if (_entries.ContainsKey(primaryKey))
        {
            var items = _entries[primaryKey];
            if (items.ContainsKey(secondaryKey))
            {
                return items[secondaryKey];
            }
        }

        return default;
    }

    public void Set(TPrimaryKey primaryKey, TSecondaryKey secondaryKey, TValue value)
    {
        if (_entries.ContainsKey(primaryKey))
        {
            var items = _entries[primaryKey];
            if (!items.ContainsKey(secondaryKey))
            {
                items.Add(secondaryKey, value);
            }
        }
        else
        {
            var item = new Dictionary<TSecondaryKey, TValue> { { secondaryKey, value } };
            _entries.Add(primaryKey, item);
        }
    }

    public int Count(TPrimaryKey primaryKey)
    {
        if (!_entries.ContainsKey(primaryKey)) return 0;
        var items = _entries[primaryKey];
        return items.Count;

    }
}