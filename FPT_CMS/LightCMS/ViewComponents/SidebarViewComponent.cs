using Microsoft.AspNetCore.Mvc;

namespace LightCMS.ViewComponents
{
    [ViewComponent]
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
