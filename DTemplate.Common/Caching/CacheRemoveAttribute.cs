using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Caching
{
    public class CacheRemoveAttribute:Attribute
    {
        public string Key { get; set; }
    }
}
