using System;
using System.Collections.Generic;
using System.Text;
using DTemplate.Common.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTemplate.Common.Test.Caching.Redis
{
    [TestClass]
    public class RedisCacheManagerTest : CacheManagerTestBase
    {
        public override ICacheManager CM => new RedisCacheManager("localhost");
    }
}
