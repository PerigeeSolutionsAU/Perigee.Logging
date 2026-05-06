using GenericLogger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<App>();
builder.Services.AddScoped<SomeService>();

builder.Services.AddNonGenericLoggerSupport();

using IHost host = builder.Build();

var app = host.Services.GetRequiredService<App>();
await app.Run();
Console.ReadKey();