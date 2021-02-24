using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DIP.Framework
{
    public class ConfigurationManager
    {
        public static IConfigurationRoot _iConfiguration;

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _iConfiguration = builder.Build();
        }

        public static string GetNode(string nodeName)
        {
            return _iConfiguration[nodeName];
        }
    }
}
