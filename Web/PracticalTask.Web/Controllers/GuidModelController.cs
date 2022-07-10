namespace PracticalTask.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PracticalTask.Data.Models;
    using PracticalTask.Services.Data;
    using PracticalTask.Web.ViewModels.GuidModel;

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

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ChangeStatus([FromBody] int guidModelid)
        {
            var result = await this.guidModelService.ChangeStatusToReadyToSaveAsync(guidModelid);

            if (!result)
            {
                return this.NotFound();
            }

            this.StatusCode(200);
            return this.Ok();
        }
    }
}
