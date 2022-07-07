namespace PracticalTask.Services.BackgroundWorkerService
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public BackgroundWorker(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Working...");
        }
    }
}