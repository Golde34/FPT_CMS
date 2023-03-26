using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Server.DAO;
using Server.DTO;
using Server.Entity;
using Server.Entity.Enum;
using Server.Repository.@interface;
using Server.Repository;
using Server.Utils;
using Microsoft.Extensions.Primitives;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private IStudentRepo studentRepo = new StudentRepository();
        private ITeacherRepo teacherRepo = new TeacherRepository();
        private ICourseRepo courseRepo = new CourseRepository();
        private IEnrollmentRepo enrollmentRepo = new EnrollmentRepository();

        [HttpGet]
        [Authorize(Roles = "Student")]
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

        [HttpGet]
        [Authorize(Roles = "Student")]
        public IActionResult GetRegisteredCourses()
        {
            // Decode the token and get the role of account
            StringValues values;
            Request.Headers.TryGetValue("Authorization", out values);
            var token = values.ToString();
            string[] tokens = token.Split(" ");


            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokens[1]);
            var accountId = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;

            Student student = studentRepo.GetStudentByAccountId(accountId.ToString());
            if (student == null)
            {
                return NotFound();
            }

            List<Enrollment> enrollments = (List<Enrollment>)enrollmentRepo.GetEnrollmentsByStudentId(student.Id);

            var courses = enrollments.Select(e => new
            {
                CourseId = e.Course.CourseId,
                CourseName = e.Course.CourseName,
                Slot = e.Course.Slot,
                SemesterId = e.Course.SemesterId,
                SubjectCode = e.Course.SubjectCode,
                TeacherId = e.Course.TeacherId
            }).ToList();

            return Ok(courses);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult GetManagedCourses()
        {
            // Decode the token and get the role of account
            StringValues values;
            Request.Headers.TryGetValue("Authorization", out values);
            var token = values.ToString();
            string[] tokens = token.Split(" ");


            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokens[1]);
            var accountId = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;

            Teacher teacher = teacherRepo.GetTeacherByAccountId(accountId.ToString());
            if (teacher == null)
            {
                return NotFound();
            }

            List<Course> courses = courseRepo.GetCourses().Where(c => c.TeacherId == teacher.Id).ToList();

            return Ok(courses);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult AddCourse(CourseDTO course)
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
        [Authorize(Roles = "Teacher")]
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

        [HttpGet("{id}")]
        public Course GetCourseByID(string id)
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

        [HttpGet]
        public Course GetLastCourse()
        {
            Course _course;
            try
            {
                var _courseManagement = new CourseManagement();
                _course = _courseManagement.GetLastCourse();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return _course;
        }
    }
}