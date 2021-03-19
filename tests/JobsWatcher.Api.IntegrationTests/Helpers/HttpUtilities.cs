using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace JobsWatcher.Api.IntegrationTests.Helpers
{
    public static class HttpUtilities
    {
        public static StringContent GetHttpContent(object value)
        {
            var payload = JsonConvert.SerializeObject(value);
            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            return httpContent;
        }
        
    }
}