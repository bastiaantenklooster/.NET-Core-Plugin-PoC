using Application.Plugins;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Runtime.Loader;

namespace Application
{
    class PluginLoader<PluginType>
    {
        public static List<PluginType> Load(string[] paths)
        {
            var exports = new List<PluginType>();

            paths.ToList().ForEach(path =>
            {
                var lc = new LoadContext(path);

                var assemblies = Directory.GetFiles(path, "*.dll")
                            .Select(lc.Load).ToList();

                var configuration = new ContainerConfiguration().WithAssemblies(assemblies);
                var container = configuration.CreateContainer();

                exports.AddRange(container.GetExports<PluginType>());
            });

            return exports;
        }
    }
}
