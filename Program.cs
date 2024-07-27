using System.Text.Json.Serialization;
using mywebapi2.Controllers;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddControllers();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


var app = builder.Build();

// using map group to set the routes

var helloApi = app.MapGroup("/hello");
helloApi.MapGet("/", () => "Hello, World!");
helloApi.MapGet("/{name}", (string name) => $"Hello, {name}!");

var mathApi = app.MapGroup("/math");
mathApi.MapGet("/sum/{a}/{b}", (int a, int b) => a + b);
mathApi.MapGet("/subtract/{a}/{b}", (int a, int b) => a - b);

// using Controller to define the routes 
app.MapControllers();

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}

