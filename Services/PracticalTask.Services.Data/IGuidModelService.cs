namespace PracticalTask.Services.Data
{
    using System.Threading.Tasks;

    using PracticalTask.Data.Models;

    public interface IGuidModelService
    {
        public Task<bool> Create(string guid);

        public Task<bool> Update(Status status, int guidModelId);

        public Task<bool> SaveAllReadyToSave();
    }
}
