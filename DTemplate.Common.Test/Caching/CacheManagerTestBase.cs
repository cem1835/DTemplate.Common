using DTemplate.Common.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DTemplate.Common.Test.Caching
{
    public abstract class CacheManagerTestBase
    {
        public abstract ICacheManager CM { get; }

        [TestMethod]
        public void AddRemoveExistsTest()
        {
            CM.Add("1", 1);
            Assert.IsTrue(CM.Exists("1"));
            Assert.AreEqual(1, CM.Get<int>("1"));
            CM.Remove("1");
            Assert.IsFalse(CM.Exists("1"));
        }

        [TestMethod]
        public void ClearTest()
        {
            CM.Add("1", 1);
            CM.Add("2", 2);
            CM.Add("3", 3);
            CM.Clear();
            Assert.IsFalse(CM.Exists("1"));
            Assert.IsFalse(CM.Exists("2"));
            Assert.IsFalse(CM.Exists("3"));
        }

        [TestMethod]
        public void AbsoluteExpirationTest()
        {
            var data = new object();
            CM.Add("object", data, TimeSpan.FromSeconds(5));
            Assert.IsTrue(CM.Exists("object"));
            Task.Delay(3000).Wait();
            Assert.IsTrue(CM.Exists("object"));
            Task.Delay(2000).Wait();
            Assert.IsFalse(CM.Exists("object"));
        }
    }
}
