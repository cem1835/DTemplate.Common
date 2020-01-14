using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;

namespace DTemplate.Common.Caching
{
    public class CacheManager : ICacheManager
    {
        protected ObjectCache Cache => MemoryCache.Default;

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public void Add(string key, object data)
        {
            Add(key, data, null);
        }

        public void Add(string key, object data, TimeSpan? cacheTime)
        {
            if (data == null)
            { return; }

            var absoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration;
            if (cacheTime.HasValue)
                absoluteExpiration = DateTimeOffset.Now + cacheTime.Value;

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = TimeSpan.FromDays(1)
            };

            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool Exists(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = Cache.Where(d => regex.IsMatch(d.Key)).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }

        public void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }

    }
}
