using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Caching
{
    public class CacheAttribute:Attribute
    {
        public int DurationMinute { get; set; }

    }
}
