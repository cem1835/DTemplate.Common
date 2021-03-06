﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Add(string key, object data);
        void Add(string key, object data, TimeSpan? cacheTime);
        bool Exists(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
        void Clear();
    }
}
