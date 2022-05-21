namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("start");
            await base.StartAsync(cancellationToken);

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("stop");
            await base.StopAsync(cancellationToken);

        }
        
    }
}