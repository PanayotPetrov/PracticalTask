namespace PracticalTask.Services.Models
{
    using PracticalTask.Data.Models;
    using PracticalTask.Services.Mapping;

    public class GuidModelDTO : IMapFrom<GuidModel>
    {
        public int Id { get; set; }

        public string Guid { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

    }
}
