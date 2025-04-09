using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using WorkerService;
using ApiCliente.Data;
using OpenTelemetry.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Configuração do DbContext
builder.Services.AddDbContext<AppDbContext>();

const string serviceName = "roll-dice";

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddConsoleExporter();
});
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter());

// Registro do serviço do Worker
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();