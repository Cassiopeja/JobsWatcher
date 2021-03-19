using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JobsWatcher.Api.Filter
{
    public class LowerCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.ToDictionary(entry => LowercaseEverythingButParameters(entry.Key),
                entry => entry.Value);
            swaggerDoc.Paths = new OpenApiPaths();
            foreach (var (key, value) in paths) swaggerDoc.Paths.Add(key, value);
        }

        private static string LowercaseEverythingButParameters(string key)
        {
            return string.Join('/', key.Split('/').Select(x => x.Contains("{") ? x : x.ToLower()));
        }
    }
}