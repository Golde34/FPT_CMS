using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace LightCMS.ViewComponents
{
    [ViewComponent]
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
