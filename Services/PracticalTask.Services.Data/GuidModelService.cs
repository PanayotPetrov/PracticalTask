namespace PracticalTask.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PracticalTask.Data.Common.Repositories;
    using PracticalTask.Data.Models;
    using PracticalTask.Services.Mapping;

    public class GuidModelService : IGuidModelService
    {
        private readonly IRepository<GuidModel> guidModelRepository;

        public GuidModelService(IRepository<GuidModel> guidModelRepository)
        {
            this.guidModelRepository = guidModelRepository;
        }

        public async Task<bool> CreateAsync(string guid)
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

        public IEnumerable<T> GetAllByStatus<T>(Status status)
        {
            return this.guidModelRepository.AllAsNoTracking().Where(x => x.Status == status).To<T>().ToList();
        }

        public async Task<bool> ChangeStatusToReadyToSaveAsync(int guidModelId)
        {
            var guidModel = this.guidModelRepository.All().FirstOrDefault(x => x.Id == guidModelId);

            if (guidModel == null || guidModel?.Status != Status.Active)
            {
                return false;
            }

            guidModel.Status = Status.ReadyToSave;

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

        public async Task<bool> UpdateStatusAsync(int id, Status status)
        {
            var guidModel = this.guidModelRepository.All().FirstOrDefault(x => x.Id == id);

            if (guidModel is null)
            {
                return false;
            }

            guidModel.Status = status;

            if (status == Status.Cancelled)
            {
                guidModel.CancelledOn = DateTime.UtcNow;
            }

            await this.guidModelRepository.SaveChangesAsync();
            return true;
        }
    }
}
