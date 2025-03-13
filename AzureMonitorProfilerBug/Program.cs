using Azure.Monitor.OpenTelemetry.AspNetCore;
using Azure.Monitor.OpenTelemetry.Profiler;
using AzureMonitorProfilerBug;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddHostedService<RequestLoopService>();

builder.Services.AddOpenTelemetry()
    .UseAzureMonitor()
    .AddAzureMonitorProfiler();

var app = builder.Build();

app.MapControllers();
app.Run();
