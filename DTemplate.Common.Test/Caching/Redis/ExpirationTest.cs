using DTemplate.Common.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DTemplate.Common.Test.Caching.Redis
{
    [TestClass]
    public class ExpirationTest
    {
        private ICacheManager cm = new RedisCacheManager("localhost");

        [TestMethod]
        public void AbsoulteExpirationTest()
        {
            var data = new object();
            cm.Add("object", data, TimeSpan.FromSeconds(5));
            Assert.IsTrue(cm.Exists("object"));
            Task.Delay(3000).Wait();
            Assert.IsTrue(cm.Exists("object"));
            Task.Delay(2000).Wait();
            Assert.IsFalse(cm.Exists("object"));
        }
    }
}
