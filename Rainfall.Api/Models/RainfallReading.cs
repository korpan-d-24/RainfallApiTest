using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;

namespace Rainfall.Api.Models;

[SwaggerSchema("Details of a rainfall reading", Title = "Rainfall reading")]
public record RainfallReading
{
    [JsonProperty("dateTime")]
    public DateTime DateMeasured { get; set; }

    [JsonProperty("value")]
    public decimal AmountMeasured { get; set; }
}