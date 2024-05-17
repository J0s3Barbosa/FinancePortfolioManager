using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace UnitTest.Services
{
    public class MockMemoryCache : IMemoryCache
    {
        private readonly Dictionary<object, object> _cache = new Dictionary<object, object>();

        public ICacheEntry CreateEntry(object key)
        {
            var entry = new MockCacheEntry(key, v => _cache[key] = v);
            _cache[key] = entry.Value;
            return entry;
        }

        public void Dispose()
        {
        }

        public void Remove(object key)
        {
            _cache.Remove(key);
        }

        public bool TryGetValue(object key, out object value)
        {
            return _cache.TryGetValue(key, out value);
        }

        private class MockCacheEntry : ICacheEntry
        {
            private readonly Action<object> _setValueCallback;

            public MockCacheEntry(object key, Action<object> setValueCallback)
            {
                Key = key;
                _setValueCallback = setValueCallback;
            }

            public void Dispose()
            {
            }

            public object Key { get; }

            private object _value;
            public object Value
            {
                get => _value;
                set
                {
                    _value = value;
                    _setValueCallback(value);
                }
            }

            public DateTimeOffset? AbsoluteExpiration { get; set; }
            public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
            public TimeSpan? SlidingExpiration { get; set; }
            public IList<IChangeToken> ExpirationTokens { get; set; } = new List<IChangeToken>();
            public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; set; } = new List<PostEvictionCallbackRegistration>();
            public CacheItemPriority Priority { get; set; } = CacheItemPriority.Normal;
            public long? Size { get; set; }
        }
    }


}