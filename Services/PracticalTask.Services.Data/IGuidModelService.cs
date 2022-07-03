namespace PracticalTask.Services.Data
{
    using PracticalTask.Data.Models;

    public interface IGuidModelService
    {
        public bool Create();

        public bool Update(Status status);

        public bool SaveAllReadyToSave();
    }
}
