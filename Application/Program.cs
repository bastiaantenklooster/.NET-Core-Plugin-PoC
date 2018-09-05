using Application.Plugins;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Runtime.Loader;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading plugins");

            #region Config
            var config = new JObject
            {
                {
                    "plugins" , new JObject {
                        {
                            "one_plugin", new JObject {
                                { "directory" , Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\OnePlugin\bin\Debug\netcoreapp2.1\") }
                            }
                        },
                        {
                            "two_plugin", new JObject {
                                { "directory" , Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\TwoPlugin\bin\Debug\netcoreapp2.1\") }
                            }
                        },
                        {
                            "three_plugin", new JObject {
                                { "directory" , Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\GoogleRetrieverPlugin\bin\Debug\netcoreapp2.1\") }
                            }
                        }
                    }
                }
            };

            var plugins = new string[] { "one_plugin", "two_plugin", "three_plugin" };
            #endregion;

            var paths = new string[plugins.Length];

            var i = 0;
            plugins.ToList().ForEach(plugin =>
                paths[i++] = config.SelectToken($"plugins.{plugin}.directory").ToString()
            );

            PluginLoader<IPlugin>.Load(paths).ForEach(export => Console.WriteLine(export.GetType().ToString() + " says: " + export.GetMessage()));

            Console.Read();
        }
    }
}
