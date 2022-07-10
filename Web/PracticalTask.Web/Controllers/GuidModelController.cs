namespace PracticalTask.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PracticalTask.Data.Models;
    using PracticalTask.Services.Data;
    using PracticalTask.Web.ViewModels.Home;

    [Authorize]
    public class GuidModelController : BaseController
    {
        private readonly IGuidModelService guidModelService;

        public GuidModelController(IGuidModelService guidModelService)
        {
            this.guidModelService = guidModelService;
        }

        public IActionResult Active()
        {
            var guidViewModels = this.guidModelService.GetAllByStatus<GuidViewModel>(Status.Active);

            var model = new GuidViewModelList
            {
                GuidViewModels = guidViewModels,
            };

            return this.View(model);
        }

        public IActionResult ReadyToSave()
        {
            var guidViewModels = this.guidModelService.GetAllByStatus<GuidViewModel>(Status.ReadyToSave);

            var model = new GuidViewModelList
            {
                GuidViewModels = guidViewModels,
            };

            return this.View(model);
        }

        public IActionResult Saved()
        {
            var guidViewModels = this.guidModelService.GetAllByStatus<GuidViewModel>(Status.Saved);

            var model = new GuidViewModelList
            {
                GuidViewModels = guidViewModels,
            };

            return this.View(model);
        }

        public IActionResult Cancelled()
        {
            var guidViewModels = this.guidModelService.GetAllByStatus<GuidViewModel>(Status.Cancelled);

            var model = new GuidViewModelList
            {
                GuidViewModels = guidViewModels,
            };

            return this.View(model);
        }
    }
}
