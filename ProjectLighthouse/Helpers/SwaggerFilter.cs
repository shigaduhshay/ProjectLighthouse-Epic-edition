using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LBPUnion.ProjectLighthouse.Helpers;

public class SwaggerFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        List<KeyValuePair<string, OpenApiPathItem>> nonApiRoutes = swaggerDoc.Paths.Where(x => !x.Key.ToLower().StartsWith("/api/v1")).ToList();
        nonApiRoutes.ForEach(x => swaggerDoc.Paths.Remove(x.Key));
    }
}