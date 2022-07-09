namespace PracticalTask.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using PracticalTask.Data.Models;
    using PracticalTask.Services.Data;
    using PracticalTask.Web.ViewModels;
    using PracticalTask.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IGuidModelService guidModelService;

        public HomeController(IGuidModelService guidModelService)
        {
            this.guidModelService = guidModelService;
        }

        public IActionResult Index()
        {
            var guidViewModels = this.guidModelService.GetAllByStatus<GuidViewModel>(Status.Active);
            var model = new GuidViewModelList
            {
                GuidViewModels = guidViewModels,
            };

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
