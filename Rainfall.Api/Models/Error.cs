using Swashbuckle.AspNetCore.Annotations;

namespace Rainfall.Api.Models;

[SwaggerSchema("Details of a rainfall reading", Title = "Error response")]
public record Error
{
    public string? Message { get; set; }

    public List<ErrorDetail>? Detail { get; set; }
}