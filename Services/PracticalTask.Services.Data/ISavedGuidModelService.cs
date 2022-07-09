namespace PracticalTask.Services.Data
{
    using System.Threading.Tasks;

    using PracticalTask.Services.Models;

    public interface ISavedGuidModelService
    {
        public Task<bool> CreateAsync(SavedGuidModelDTO model);
    }
}
