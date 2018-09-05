using System.Composition;
using Application.Plugins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OnePlugin
{
    [Export(typeof(IPlugin))]
    public class OnePlugin : IPlugin
    {
        public string GetMessage()
        {
            var obj = new JObject
            {
                { "message", "This message comes from OnePlugin" }
            };

            return obj.SelectToken("message").ToObject<string>();
        }
    }
}