using Shared;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "2.0",
        Title = "API (Swagger 2.0)",
        Description = "OpenAPI 2.0 (Swagger 2.0) documentation"
    });
    options.SwaggerDoc("v3", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "3.0",
        Title = "API (OpenAPI 3.0)",
        Description = "OpenAPI 3.0 documentation"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API (Swagger 2.0)");
        options.SwaggerEndpoint("/swagger/v3/swagger.json", "API (OpenAPI 3.0)");
    });
}

// Disable HTTPS redirection
//// app.UseHttpsRedirection();

app.MapPost("/api/v1/parse", async (HttpRequest request) =>
{
    try
    {
        using var reader = new StreamReader(request.Body, Encoding.UTF8);
        var body = await reader.ReadToEndAsync();
        var parsed = JSON.Parse(body);

        return Results.Content(parsed.ToString(), "application/json");
    }
    catch (Exception ex)
    {
        return Results.Json(new { message = ex.Message, code = 400 }, statusCode: 400);
    }
})
.Accepts<string>("text/plain");

app.Run();
