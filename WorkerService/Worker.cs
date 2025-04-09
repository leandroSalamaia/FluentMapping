using ApiCliente.Data;

namespace WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly AppDbContext _context;
    private readonly TimeSpan _intervalo = TimeSpan.FromMinutes(1); // Variável para o tempo do worker

    public Worker(ILogger<Worker> logger, AppDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker executando às: {time}", DateTimeOffset.Now);
            }

            await DoWork(stoppingToken);
            await Task.Delay(_intervalo, stoppingToken);
        }
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Executando tarefa no DoWork...");
        await Task.CompletedTask;
    }
}