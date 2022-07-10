namespace PracticalTask.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PracticalTask.Data.Common.Models;

    public class GuidModel : BaseModel<int>
    {
        [Required]
        public string Guid { get; set; }

        public Status Status { get; set; }

        public int? GuidFileModelId { get; set; }

        public GuidFileModel GuidFileModel { get; set; }
    }
}
