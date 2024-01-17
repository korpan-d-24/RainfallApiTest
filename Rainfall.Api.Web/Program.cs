using System.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Rainfall.Api.Web.Services;
using Rainfall.Api.Web.Shared;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddHttpClient("RainFallApiBaseUrl", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("RainfallApi:BaseUrl"));
    c.DefaultRequestHeaders.Accept.Clear();
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddScoped(typeof(IRainFallReaderService), typeof(RainFallReaderService));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Rainfall Api",
        Version = "1.0",
        Contact = new OpenApiContact
        {
            Name = "Full code",
            Url = new Uri("https://github.com/korpan-d-24/RainfallApiTest")
        },
        Description = "An API which provides rainfall reading data"
    });
    options.AddServer(new OpenApiServer
    {
        Url = "http://localhost:3000",
        Description = "Rainfall API"
    });
    options.DocumentFilter<TagDescriptionsDocumentFilter>();
    options.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

