using Microsoft.AspNetCore.Mvc;

namespace LightCMS.ViewComponents
{
    [ViewComponent]
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
