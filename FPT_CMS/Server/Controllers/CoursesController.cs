using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoursesController : Controller
    {
        [HttpGet]
        public IActionResult GetCourses()
        {
            return Ok();
        }
    }
}
