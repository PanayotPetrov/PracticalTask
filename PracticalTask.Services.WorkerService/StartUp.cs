namespace PracticalTask.Services.WorkerService
{
    using PracticalTask.Services.Data;

    public class StartUp
    {
        private readonly IGuidModelService guidModelService;

        public StartUp(IGuidModelService guidModelService)
        {
            this.guidModelService = guidModelService;
        }

        public async Task RunAsync()
        {
            var guid = Guid.NewGuid().ToString();
            await this.guidModelService.Create(guid);
        }
    }
}
