using Swashbuckle.AspNetCore.Annotations;

namespace Rainfall.Api.Models;

[SwaggerSchema("Details of invalid request property")]
public record ErrorDetail
{
    public string? PropertyName { get; set; }

    public string? Message { get; set; }
}