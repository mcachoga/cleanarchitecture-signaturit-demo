using Signaturit.Web.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Signaturit.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : BaseController<HomeController>
    {
        public IActionResult Index()
        {
            _notify.Information("Welcome user!");
            return View();
        }
    }
}