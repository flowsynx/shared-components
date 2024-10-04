namespace FlowSynx.IO.Cache;

public class FlowSynxCache<TKey, TValue> : ICache<TKey, TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _entries;

    public FlowSynxCache()
    {
        _entries = new Dictionary<TKey, TValue>();
    }

    public TValue? Get(TKey key)
    {
        if (_entries.ContainsKey(key))
            return _entries[key];
        
        return default;
    }

    public void Set(TKey key, TValue value)
    {
        if (_entries.ContainsKey(key))
            _entries[key] = value;
        else
            _entries.Add(key, value);
    }

    public void Delete(TKey key)
    {
        if (!_entries.ContainsKey(key)) 
            return;

        _entries.Remove(key);
    }

    public int Count()
    {
        return _entries.Count;
    }
}