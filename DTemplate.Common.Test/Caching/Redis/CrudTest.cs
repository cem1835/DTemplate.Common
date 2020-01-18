using DTemplate.Common.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Test.Caching.Redis
{
    [TestClass]
    public class CrudTest
    {
        private ICacheManager cm = new RedisCacheManager("localhost");

        [TestMethod]
        public void AddRemoveExistsTest()
        {
            cm.Add("1", 1);
            Assert.IsTrue(cm.Exists("1"));
            Assert.AreEqual(1, cm.Get<int>("1"));
            cm.Remove("1");
            Assert.IsFalse(cm.Exists("1"));
        }

        [TestMethod]
        public void ClearTest()
        {
            cm.Add("1", 1);
            cm.Add("2", 2);
            cm.Add("3", 3);
            cm.Clear();
            Assert.IsFalse(cm.Exists("1"));
            Assert.IsFalse(cm.Exists("2"));
            Assert.IsFalse(cm.Exists("3"));
        }
    }
}
