using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Server.DAO;
using Server.Entity;
using Server.Entity.Enum;
using Server.Utils;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoursesController : Controller
    {
        [HttpGet]
        public ActionResult<List<Course>> GetCourses()
        {
            // string? tokenParse = DecodeJwtToken.GetRoleFromToken(Request.Headers[HeaderNames.Authorization]);
            // if (tokenParse == null)
            // {
            //     return Unauthorized();
            // }
            // if (!tokenParse.Equals(Roles.Teacher.ToString()))
            // {
            //     return Forbid();
            // }
            List<Course> _courses;
            try
            {
                var _courseManagement = new CourseManagement();
                _courses = _courseManagement.GetCourses().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return _courses;
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            // string? tokenParse = DecodeJwtToken.GetRoleFromToken(Request.Headers[HeaderNames.Authorization]);
            // if (tokenParse == null)
            // {
            //     return Unauthorized();
            // }
            // if (!tokenParse.Equals(Roles.Teacher.ToString()))
            // {
            //     return Forbid();
            // }
            if (course == null)
            {
                return BadRequest();
            }

            var _courseManagement = new CourseManagement();
            _courseManagement.AddCourse(course);
            return Ok();
        }

        [HttpPost]
        public IActionResult EditCourse(Course course)
        {
            // string? tokenParse = DecodeJwtToken.GetRoleFromToken(Request.Headers[HeaderNames.Authorization]);
            // if (tokenParse == null)
            // {
            //     return Unauthorized();
            // }
            // if (!tokenParse.Equals(Roles.Teacher.ToString()))
            // {
            //     return Forbid();
            // }
            if (course == null)
            {
                return BadRequest();
            }

            var _courseManagement = new CourseManagement();
            _courseManagement.UpdateCourse(course);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public Course GetCourseByID(int id)
        {
            Course _course;
            try
            {
                var _courseManagement = new CourseManagement();
                _course = _courseManagement.GetCourseById(id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return _course;
        }
    }
}