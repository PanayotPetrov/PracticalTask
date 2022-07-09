namespace PracticalTask.Services.Models
{

    using PracticalTask.Data.Models;
    using PracticalTask.Services.Mapping;

    public class GuidFileModelDTO : IMapTo<GuidFileModel>
    {
        public string FileName { get; set; }

        public string FileContent { get; set; }

        public ICollection<int> GuidModelIds { get; set; }
    }
}
