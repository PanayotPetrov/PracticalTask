namespace PracticalTask.Services.Data
{
    using System.Threading.Tasks;

    using PracticalTask.Data.Common.Repositories;
    using PracticalTask.Data.Models;
    using PracticalTask.Services.Models;

    public class SavedGuidModelService : ISavedGuidModelService
    {
        private readonly IRepository<SavedGuidModel> savedGuidModelRepository;

        public SavedGuidModelService(IRepository<SavedGuidModel> savedGuidModelRepository)
        {
            this.savedGuidModelRepository = savedGuidModelRepository;
        }

        public async Task<bool> CreateAsync(GuidModelDTO model)
        {
            var savedGuidModel = new SavedGuidModel
            {

            };

            await this.savedGuidModelRepository.AddAsync(savedGuidModel);
            await this.savedGuidModelRepository.SaveChangesAsync();
            return true;
        }
    }
}
