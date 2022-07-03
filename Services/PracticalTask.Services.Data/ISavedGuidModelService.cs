namespace PracticalTask.Services.Data
{
    using System.Threading.Tasks;

    public interface ISavedGuidModelService
    {
        public Task<bool> Create();
    }
}
