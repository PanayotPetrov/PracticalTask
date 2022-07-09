namespace PracticalTask.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PracticalTask.Data.Common.Models;

    public class GuidFileModel : BaseModel<int>
    {
        public GuidFileModel()
        {
            this.GuidModels = new HashSet<GuidModel>();
        }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileContent { get; set; }

        public ICollection<GuidModel> GuidModels { get; set; }
    }
}
