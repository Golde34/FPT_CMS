using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class BaseController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public BaseController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult GetWebRootPath()
        {
            string webRootPath = _env.WebRootPath;
            return Ok(webRootPath);
        }
    }
}
