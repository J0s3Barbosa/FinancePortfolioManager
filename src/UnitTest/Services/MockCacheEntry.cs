using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace UnitTest.Services
{
    public class MockCacheEntry : ICacheEntry
    {
        public void Dispose() { }

        public object Key { get; set; }

        public object Value { get; set; }

        public DateTimeOffset? AbsoluteExpiration { get; set; }

        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

        public TimeSpan? SlidingExpiration { get; set; }

        public IList<IChangeToken> ExpirationTokens { get; set; } = new List<IChangeToken>();

        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; set; } = new List<PostEvictionCallbackRegistration>();

        public CacheItemPriority Priority { get; set; } = CacheItemPriority.Normal;

        public long? Size { get; set; }



    }

}