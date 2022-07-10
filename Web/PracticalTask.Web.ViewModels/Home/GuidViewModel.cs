namespace PracticalTask.Web.ViewModels.Home
{
    using System;

    using AutoMapper;
    using PracticalTask.Data.Models;
    using PracticalTask.Services.Mapping;

    public class GuidViewModel : IMapFrom<GuidModel>, IHaveCustomMappings
    {
        public string Guid { get; set; }

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<GuidModel, GuidViewModel>()
                .ForMember(x => x.Status, opt => opt.MapFrom(y => y.Status.ToString()));
        }
    }
}
