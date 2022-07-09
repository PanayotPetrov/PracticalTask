namespace PracticalTask.Services.Data
{
    using System;
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

        public async Task<bool> CreateAsync(SavedGuidModelDTO model)
        {
            var savedGuidModel = new SavedGuidModel
            {
                FileName = model.FileName,
                FileData = model.FileData,
                GuidModelId = model.GuidModelId,
                Status = Status.Saved,
                SavedOn = DateTime.UtcNow,
            };

            try
            {
                await this.savedGuidModelRepository.AddAsync(savedGuidModel);
                await this.savedGuidModelRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
