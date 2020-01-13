using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Helper
{
    public class CollectionNameAttribute:Attribute
    {
        public string TableName { get; set; }

        public CollectionNameAttribute(string tableName)
        {
            TableName = tableName;
        }

    }
}
