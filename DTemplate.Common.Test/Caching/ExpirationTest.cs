using DTemplate.Common.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DTemplate.Common.Test.Caching
{
    [TestClass]
    public class ExpirationTest
    {
        [TestMethod]
        public void AbsoluteExpirationTest()
        {
            var cacheManager = new CacheManager();
            var data = new object();
            cacheManager.Add("object", data, TimeSpan.FromSeconds(5));
            Assert.IsTrue(cacheManager.Exists("object"));
            Task.Delay(3000).Wait();
            Assert.IsTrue(cacheManager.Exists("object"));
            Task.Delay(2000).Wait();
            Assert.IsFalse(cacheManager.Exists("object"));
        }
    }
}
