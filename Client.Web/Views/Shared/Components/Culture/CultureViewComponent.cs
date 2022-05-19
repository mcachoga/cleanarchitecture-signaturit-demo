using Microsoft.AspNetCore.Mvc;

namespace Signaturit.Web.Views.Shared.Components.Culture
{
    public class CultureViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}