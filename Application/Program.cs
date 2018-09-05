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

            var config = new JObject
            {
                {
                    "plugins" , new JObject {
                        {
                            "one_plugin", new JObject {
                                { "directory" , @"C:\Users\Bastiaan\source\repos\Application\OnePlugin\bin\Debug\netcoreapp2.1\" }
                            }
                        },
                        {
                            "two_plugin", new JObject {
                                { "directory" , @"C:\Users\Bastiaan\source\repos\Application\TwoPlugin\bin\Debug\netcoreapp2.1\" }
                            }
                        },
                        {
                            "three_plugin", new JObject {
                                { "directory" , @"C:\Users\Bastiaan\source\repos\Application\GoogleRetrieverPlugin\bin\Debug\netcoreapp2.1\" }
                            }
                        }
                    }
                }
            };

            var plugins = new string[] { "one_plugin", "two_plugin", "three_plugin" };

            var exports = new List<IPlugin>();

            plugins.ToList().ForEach(plugin =>
            {
                var assemblies = Directory.GetFiles(config.SelectToken($"plugins.{plugin}.directory").ToString(), "*.dll")
                            .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath).ToList();

                var configuration = new ContainerConfiguration().WithAssemblies(assemblies);
                var container = configuration.CreateContainer();

                exports.AddRange(container.GetExports<IPlugin>());
            });

            foreach (IPlugin export in exports)
            {
                Console.WriteLine(export.GetType().ToString() + " says: " + export.GetMessage());
            }

            Console.Read();
        }
    }
}
