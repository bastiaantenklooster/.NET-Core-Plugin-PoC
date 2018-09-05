using System.Composition;
using Application.Plugins;
using Newtonsoft.Json.Linq;

namespace TwoPlugin
{
    [Export(typeof(IPlugin))]
    public class TwoPlugin : IPlugin
    {
        public string GetMessage()
        {
            var obj = new JObject
            {
                { "message", "This is a message defined in TwoPlugin" }
            };
            return obj.SelectToken("message").ToString();
        }
    }
}
