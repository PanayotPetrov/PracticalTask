namespace PracticalTask.Services.BackgroundWorkerService
{
    public sealed class BackgroundWorker : IHostedService, IAsyncDisposable
    {
        private readonly Task _completedTask = Task.CompletedTask;
        private readonly IServiceProvider serviceProvider;
        private readonly int minutesOnWhichToStartService;
        private Timer? timer;

        public BackgroundWorker(IServiceProvider serviceProvider, IConfiguration config)
        {
            this.serviceProvider = serviceProvider;
            this.minutesOnWhichToStartService = int.Parse(config["BackgroundServiceStartMinutes"]);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            this.timer = new Timer(this.DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(this.minutesOnWhichToStartService));

            return _completedTask;
        }

        private void DoWork(object? state)
        {
            Console.WriteLine("Working......");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);

            return _completedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (this.timer is IAsyncDisposable timer)
            {
                await timer.DisposeAsync();
            }

            this.timer = null;
        }
    }
}
