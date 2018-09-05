using Application.Plugins;
using System.Composition;
using System.Net.Http;

namespace GoogleRetrieverPlugin
{
    [Export(typeof(IPlugin))]
    public class GoogleRetrieverPlugin : IPlugin
    {
        public string GetMessage()
        {
            string html = string.Empty;
            string url = @"https://www.google.com";

            var client = new HttpClient();

            var result = client.GetAsync(url);


            return "t" + result.Result.Content.ReadAsStringAsync().Result.Substring(0, 256).ToString();
        }
    }
}
