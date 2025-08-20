using Shared;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Disable HTTPS redirection
//// app.UseHttpsRedirection();

app.MapPost("/api/v1/parse", async (HttpRequest request) =>
{
    try
    {
        using var reader = new StreamReader(request.Body, Encoding.UTF8);
        var json = await reader.ReadToEndAsync();
        var parsed = JSON.Parse(json);

        return Results.Content(parsed.ToString(), "application/json");
    }
    catch (Exception ex)
    {
        return Results.Json(new { message = ex.Message, code = 400 }, statusCode: 400);
    }
});

app.Run();
