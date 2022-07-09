using AutoMapper;
using PracticalTask.Services.Mapping;

namespace PracticalTask.Services.Models
{
    public class SavedGuidModelDTO : IMapFrom<GuidModelDTO>, IHaveCustomMappings
    {
        public string FileName { get; set; }

        public byte[] FileData { get; set; }

        public int GuidModelId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<GuidModelDTO, SavedGuidModelDTO>()
                .ForMember(x => x.FileName, opt => opt.MapFrom(y => y.Guid))
                .ForMember(x => x.GuidModelId, opt => opt.MapFrom(y => y.Id));
        }
    }
}
