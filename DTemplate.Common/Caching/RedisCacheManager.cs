using DTemplate.Common.Serialization;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Caching
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly ConnectionMultiplexer redis;

        public RedisCacheManager(string redisServer)
        {
            redis = ConnectionMultiplexer.Connect(redisServer);
        }

        public void Add(string key, object data)
        {
            Add(key, data, null);
        }

        public void Add(string key, object data, TimeSpan? cacheTime)
        {
            var db = redis.GetDatabase();
            var seri = Json.Serialize(data);
            db.StringSet(key, seri, cacheTime);
        }

        public void Clear()
        {
            var db = redis.GetDatabase();
            db.Execute("FLUSHALL");
        }

        public bool Exists(string key)
        {
            var db = redis.GetDatabase();
            return db.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            var db = redis.GetDatabase();
            var seri = db.StringGet(key);
            var data = Json.Deserialize<T>(seri);
            return data;
        }

        public void Remove(string key)
        {
            var db = redis.GetDatabase();
            db.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }
    }
}
