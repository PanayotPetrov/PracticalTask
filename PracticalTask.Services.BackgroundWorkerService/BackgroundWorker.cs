namespace PracticalTask.Services.BackgroundWorkerService
{
    using PracticalTask.Services.Data;
    using PracticalTask.Services.Models;

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
            this.timer = new Timer(async (e) => { await DoWork(e); }, null, TimeSpan.Zero, TimeSpan.FromMinutes(this.minutesOnWhichToStartService));

            return _completedTask;
        }

        private async Task DoWork(object? state)
        {
            using (var serviceScope = this.serviceProvider.CreateScope())
            {
                var savedGuidModelService = serviceScope.ServiceProvider.GetRequiredService<ISavedGuidModelService>();
                var guidModelService = serviceScope.ServiceProvider.GetRequiredService<IGuidModelService>();

                var guidModels = guidModelService.GetAllReadyToSave<GuidModelDTO>();

                foreach (var guidModel in guidModels)
                {

                    // TO DO: Check if GuidModel status should be cancelled
                    // TO DO: Create file
                    // TO DO: Change status of Guid Model
                    // TO DO: Create SavedGuidModel

                }
            }
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
