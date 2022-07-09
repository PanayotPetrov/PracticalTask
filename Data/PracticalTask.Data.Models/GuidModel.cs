namespace PracticalTask.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PracticalTask.Data.Common.Models;

    public class GuidModel : BaseModel<int>
    {
        [Required]
        public string Guid { get; set; }

        public Status Status { get; set; }

        public DateTime? ReadyToBeSavedOn { get; set; }

        public DateTime? CancelledOn { get; set; }
    }
}
