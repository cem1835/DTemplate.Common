using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Serialization
{
    public static class Json
    {
        public static string Serialize(object data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        public static T Deserialize<T>(string seri)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(seri);
        }
    }
}
