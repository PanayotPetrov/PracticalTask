namespace PracticalTask.Services.BackgroundWorkerService
{
    using System.Text;

    using PracticalTask.Data.Models;
    using PracticalTask.Services.Data;
    using PracticalTask.Services.Models;

    public sealed class BackgroundWorker : IHostedService, IAsyncDisposable
    {
        private readonly Task _completedTask = Task.CompletedTask;
        private readonly IServiceProvider serviceProvider;
        private readonly string rootPath;
        private readonly double minutesOnWhichToStartService = 10;
        private Timer? timer;

        public BackgroundWorker(IServiceProvider serviceProvider, IConfiguration config, IHostEnvironment environment)
        {
            this.serviceProvider = serviceProvider;
            this.rootPath = $"{environment.ContentRootPath}/guids/";

            if (double.TryParse(config["BackgroundServiceStartMinutes"], out var minutesOnWhichToStartService))
            {
                this.minutesOnWhichToStartService = minutesOnWhichToStartService;
            };
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
                var guidModelService = serviceScope.ServiceProvider.GetRequiredService<IGuidModelService>();
                var guidFileModelService = serviceScope.ServiceProvider.GetRequiredService<IGuidFileModelService>();
                var readyToSaveGuidModels = guidModelService.GetAllByStatus<GuidModelDTO>(Status.ReadyToSave);
                var guidModelIds = new List<int>();
                var sb = new StringBuilder();

                foreach (var guidModel in readyToSaveGuidModels)
                {
                    if (DateTime.UtcNow - guidModel.ModifiedOn >= TimeSpan.FromMinutes(2 * this.minutesOnWhichToStartService))
                    {
                        await guidModelService.UpdateStatusAsync(guidModel.Id, Status.Cancelled);
                        continue;
                    }

                    guidModelIds.Add(guidModel.Id);
                    sb.AppendLine(guidModel.Guid);
                }

                if (guidModelIds.Count > 0)
                {
                    Directory.CreateDirectory($"{rootPath}");

                    var fileName = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

                    using (FileStream fs = File.Create($"{rootPath}{fileName}.txt"))
                    {
                        byte[] guidData = new UTF8Encoding(true).GetBytes(sb.ToString());
                        await fs.WriteAsync(guidData);
                    }

                    var model = new GuidFileModelDTO
                    {
                        FileContent = sb.ToString(),
                        FileName = fileName,
                        GuidModelIds = guidModelIds,
                    };

                    await guidFileModelService.CreateAsync(model);
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
