namespace PracticalTask.Services.BackgroundService
{
    using PracticalTask.Services.Data;

    public class StartUp
    {
        private readonly IGuidModelService guidModelService;
        private readonly ISavedGuidModelService savedGuidModelService;


        public StartUp(IGuidModelService guidModelService, ISavedGuidModelService savedGuidModelService)
        {
            this.guidModelService = guidModelService;
            this.savedGuidModelService = savedGuidModelService;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Running");
        }
    }
}
