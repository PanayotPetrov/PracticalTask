namespace PracticalTask.Services.Data
{
    using System.Threading.Tasks;

    using PracticalTask.Services.Models;

    public interface IGuidFileModelService
    {
        public Task<bool> CreateAsync(GuidFileModelDTO model);
    }
}
