namespace PracticalTask.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PracticalTask.Data.Common.Models;

    public class SavedGuidModel : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        public DateTime SavedOn { get; set; }

        public Status Status { get; set; }

        public byte[] FileData { get; set; }

        public int GuidModelId { get; set; }

        public GuidModel GuidModel { get; set; }
    }
}
