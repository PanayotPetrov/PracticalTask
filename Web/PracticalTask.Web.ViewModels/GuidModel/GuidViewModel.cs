namespace PracticalTask.Web.ViewModels.GuidModel
{
    using System;

    using AutoMapper;
    using PracticalTask.Data.Models;
    using PracticalTask.Services.Mapping;

    public class GuidViewModel : IMapFrom<GuidModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Guid { get; set; }

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string SavedFileName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<GuidModel, GuidViewModel>()
                .ForMember(x => x.Status, opt => opt.MapFrom(y => y.Status.ToString()))
                .ForMember(x => x.SavedFileName, opt => opt.MapFrom(y =>
                    y.GuidFileModel == null
                    ? null
                    : y.GuidFileModel.FileName));
        }
    }
}
