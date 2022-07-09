namespace PracticalTask.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PracticalTask.Data.Common.Repositories;
    using PracticalTask.Data.Models;
    using PracticalTask.Services.Mapping;
    using PracticalTask.Services.Models;

    public class GuidFileModelService : IGuidFileModelService
    {
        private readonly IRepository<GuidFileModel> guidFileModelRepository;
        private readonly IRepository<GuidModel> guidModelRepository;

        public GuidFileModelService(IRepository<GuidFileModel> guidFileModelRepository, IRepository<GuidModel> guidModelRepository)
        {
            this.guidFileModelRepository = guidFileModelRepository;
            this.guidModelRepository = guidModelRepository;
        }

        public async Task<bool> CreateAsync(GuidFileModelDTO model)
        {
            var guidFileModel = AutoMapperConfig.MapperInstance.Map<GuidFileModel>(model);

            var guidModels = this.guidModelRepository.All().Where(x => x.Status == Status.Saved).ToList();

            foreach (var guidModel in guidModels)
            {
                guidFileModel.GuidModels.Add(guidModel);
            }

            try
            {
                await this.guidFileModelRepository.AddAsync(guidFileModel);
                await this.guidFileModelRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
