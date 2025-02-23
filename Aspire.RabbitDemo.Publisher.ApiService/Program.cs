using Aspire.RabbitDemo.ServiceDefaults;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new OpenApiInfo
        {
            Version = "v1",
            Title = "Weather Api",
            Description = "An ASP.NET Core Web API for publishing random generated weather on a RabbitMQ." +
            "You can pass an empty body to generate random data or introduce your data to be published",
        };
    };
});
builder.AddRabbitMQClient(RabbitStatic.ConnectionName, settings =>
{
    settings.ConnectionString = RabbitStatic.ConnectionString;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapPost("/weatherforecast", (IConnection connection, [FromBody] WeatherForecast[]? request = default) =>
{
    request ??= [.. Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))];

    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));

    using var channel = connection.CreateModel();
    channel.QueueDeclare(RabbitStatic.QueueName, durable: true, exclusive: false, autoDelete: true);
    channel.BasicPublish(exchange: string.Empty, RabbitStatic.QueueName, true, channel.CreateBasicProperties(), body);

    return request;
})
.WithName("GetWeatherForecast");

if (app.Environment.IsEnvironment("Development"))
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.MapDefaultEndpoints();

app.Run();
