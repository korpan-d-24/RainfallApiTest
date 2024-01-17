using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Rainfall.Api.Models;

[SwaggerSchema("Details of a rainfall reading", Title = "Rainfall reading response")]
public record RainfallReadingResponse
{
    [JsonProperty("items")]
    public List<RainfallReading>? Readings { get; set; }
}