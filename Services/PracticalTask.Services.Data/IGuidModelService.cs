namespace PracticalTask.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PracticalTask.Data.Models;

    public interface IGuidModelService
    {
        public Task<bool> CreateAsync(string guid);

        public Task<bool> ChangeStatusToReadyToSaveAsync(int guidModelId);

        public IEnumerable<T> GetAllByStatus<T>(Status status);

        public Task<bool> UpdateStatusAsync(int id, Status status);
    }
}
