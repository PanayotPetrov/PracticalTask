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
        public void Run()
        {
            Console.WriteLine("Running...");
        }
    }
}
