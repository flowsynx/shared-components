namespace FlowSynx.IO.Cache;

public interface ICache<in TKey, TValue>
{
    TValue? Get(TKey key);
    void Set(TKey key, TValue value);
    void Delete(TKey key);
    int Count();
}