namespace AvaloniaPdfViewer.Internals;

internal class DisposingLimitCache<TKey, TValue>(int maxSize)
    where TValue : IDisposable where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _cache = new();
    private readonly LinkedListQueue<TKey> _order = new();

    public void Add(TKey key, TValue value)
    {
        if (_cache.Count >= maxSize)
        {
            var oldestKey = _order.Dequeue();
            if (_cache.Remove(oldestKey, out var oldestValue))
            {
                oldestValue.Dispose();
            }
        }
        
        _cache[key] = value;
        _order.Enqueue(key);
    }

    public bool TryGetValue(TKey key, out TValue? value)
    {
        return _cache.TryGetValue(key, out value);
    }
    
    public TValue? Get(TKey key)
    {
        _cache.TryGetValue(key, out var value);
        return value;
    }

    public bool ContainsKey(TKey key)
    {
        return _cache.ContainsKey(key);
    }

    public TValue? RemoveKey(TKey key)
    {
        if (_cache.Remove(key, out var value))
        {
            _order.Remove(key);
            value.Dispose();
        }

        return value;
    }
}