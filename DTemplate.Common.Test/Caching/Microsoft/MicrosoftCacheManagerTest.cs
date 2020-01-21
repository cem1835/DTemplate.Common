using System;
using System.Collections.Generic;
using System.Text;
using DTemplate.Common.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTemplate.Common.Test.Caching.Microsoft
{
    [TestClass]
    public class MicrosoftCacheManagerTest : CacheManagerTestBase
    {
        public override ICacheManager CM => new MicrosoftCacheManager();
    }
}
