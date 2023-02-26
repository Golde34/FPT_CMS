using Microsoft.AspNetCore.Mvc;

namespace LightCMS.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
