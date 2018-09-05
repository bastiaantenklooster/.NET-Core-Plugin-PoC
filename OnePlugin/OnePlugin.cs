using System.Composition;
using Application.Plugins;

namespace OnePlugin
{
    [Export(typeof(IPlugin))]
    public class OnePlugin : IPlugin
    {
        public string GetMessage()
        {
            return new Message("Hello this is OnePlugin speaking!!!").GetBody();
        }
    }
}