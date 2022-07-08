namespace PracticalTask.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGuidModelService
    {
        public Task<bool> CreateAsync(string guid);

        public Task<bool> ChangeStatusToReadyToSaveAsync(int guidModelId);

        public IEnumerable<T> GetAllReadyToSave<T>();

        public IEnumerable<T> GetAllActive<T>();
    }
}
