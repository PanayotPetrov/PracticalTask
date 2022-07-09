namespace PracticalTask.Data.Models
{
    using System.Collections.Generic;

    using PracticalTask.Data.Common.Models;

    public class GuidFileModel : BaseModel<int>
    {
        public GuidFileModel()
        {
            this.GuidModels = new HashSet<GuidModel>();
        }

        public string FileName { get; set; }

        public string FileContent { get; set; }

        public ICollection<GuidModel> GuidModels { get; set; }
    }
}
