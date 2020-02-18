using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DTemplate.Common.Configs
{
    public static class ConfigReader
    {
        private static IConfiguration configuration;
        static ConfigReader()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public static string Get(string name)
            => configuration[name];
    }
}
