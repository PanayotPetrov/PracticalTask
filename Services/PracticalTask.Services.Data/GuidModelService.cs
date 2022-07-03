namespace PracticalTask.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PracticalTask.Data.Common.Repositories;
    using PracticalTask.Data.Models;

    public class GuidModelService : IGuidModelService
    {
        private readonly IRepository<GuidModel> guidModelRepository;

        public GuidModelService(IRepository<GuidModel> guidModelRepository)
        {
            this.guidModelRepository = guidModelRepository;
        }

        public async Task<bool> Create(string guid)
        {
            var guidModel = new GuidModel
            {
                Guid = guid,
                Status = Status.Active,
            };

            try
            {
                await this.guidModelRepository.AddAsync(guidModel);
                await this.guidModelRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SaveAllReadyToSave()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Status status, int guidModelId)
        {
            var guidModel = this.guidModelRepository.All().FirstOrDefault(x => x.Id == guidModelId);

            guidModel.Status = status;

            try
            {
                await this.guidModelRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
