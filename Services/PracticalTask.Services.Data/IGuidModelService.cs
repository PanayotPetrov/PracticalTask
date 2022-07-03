namespace PracticalTask.Services.Data
{
    using System.Threading.Tasks;

    using PracticalTask.Data.Models;

    public interface IGuidModelService
    {
        public Task<bool> Create();

        public Task<bool> Update(Status status);

        public Task<bool> SaveAllReadyToSave();
    }
}
