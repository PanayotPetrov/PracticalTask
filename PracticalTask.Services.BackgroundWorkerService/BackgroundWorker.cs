namespace PracticalTask.Services.BackgroundWorkerService
{
    using PracticalTask.Data.Models;
    using PracticalTask.Services.Data;
    using PracticalTask.Services.Models;

    public sealed class BackgroundWorker : IHostedService, IAsyncDisposable
    {
        private readonly Task _completedTask = Task.CompletedTask;
        private readonly IServiceProvider serviceProvider;
        private readonly string rootPath;
        private readonly int minutesOnWhichToStartService;
        private Timer? timer;

        public BackgroundWorker(IServiceProvider serviceProvider, IConfiguration config, IHostEnvironment environment)
        {
            this.serviceProvider = serviceProvider;
            this.rootPath = $"{environment.ContentRootPath}/guids/";
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

                var readyToSaveguidModels = guidModelService.GetAllByStatus<GuidModelDTO>(Status.ReadyToSave);

                foreach (var guidModel in readyToSaveguidModels)
                {
                    if (DateTime.UtcNow - guidModel.CreatedOn >= TimeSpan.FromMinutes(20))
                    {
                        await guidModelService.UpdateStatusAsync(guidModel.Id, Status.Cancelled);
                    }
                    else
                    {
                        //TO DO: Create file

                        await guidModelService.UpdateStatusAsync(guidModel.Id, Status.Saved);
                        //TO DO: Create create GuidFileModel
                    }
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
