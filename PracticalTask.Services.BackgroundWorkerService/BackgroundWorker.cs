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
                var guidModelService = serviceScope.ServiceProvider.GetRequiredService<IGuidModelService>();
                var guidFileModelService = serviceScope.ServiceProvider.GetRequiredService<IGuidFileModelService>();

                var readyToSaveGuidModels = guidModelService.GetAllByStatus<GuidModelDTO>(Status.ReadyToSave);

                var guidModelIds = new List<int>();
                var sb = new StringBuilder();

                foreach (var guidModel in readyToSaveGuidModels)
                {
                    var status = Status.Cancelled;

                    if (DateTime.UtcNow - guidModel.CreatedOn < TimeSpan.FromMinutes(20))
                    {
                        status = Status.Saved;
                        guidModelIds.Add(guidModel.Id);
                        sb.AppendLine(guidModel.Guid);
                    }

                    await guidModelService.UpdateStatusAsync(guidModel.Id, status);
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
