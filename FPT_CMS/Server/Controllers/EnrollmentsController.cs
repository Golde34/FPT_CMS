using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Server.DAO;
using Server.DTO;
using Server.Entity;
using Server.Repository;
using Server.Repository.@interface;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EnrollmentsController : Controller
    {
        private IStudentRepo studentRepo = new StudentRepository();
        private ICourseRepo courseRepo = new CourseRepository();
        private IEnrollmentRepo enrollmentRepo = new EnrollmentRepository();

        [HttpPost]
        [Authorize(Roles = "Student")]
        public IActionResult AddEnroll(EnrollDTO enrollDTO)
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

            Course course = courseRepo.GetCourseById(enrollDTO.id);
            if (course == null)
            {
                return NotFound();
            }

            Enrollment enrollment = new Enrollment();
            enrollment.Id = 0;
            enrollment.StudentId = student.Id;
            enrollment.CourseId = course.CourseId;

            try
            {
                enrollmentRepo.AddEnrollment(enrollment);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Student")]
        public IActionResult DeleteEnroll(string courseId)
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

            Course course = courseRepo.GetCourseById(courseId);
            if (course == null)
            {
                return NotFound();
            }

            Enrollment enrollment = enrollmentRepo.GetEnrollmentsByStudentId(student.Id).FirstOrDefault(e => e.CourseId.Equals(course.CourseId));

            if(enrollment == null)
            {
                return Conflict();
            }

            enrollmentRepo.DeleteEnrollment(enrollment);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public IActionResult IsEnrolledIn(string courseId)
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

            Course course = courseRepo.GetCourseById(courseId);
            if (course == null)
            {
                return NotFound();
            }

            Enrollment enrollment = enrollmentRepo.GetEnrollmentsByStudentId(student.Id).FirstOrDefault(e => e.CourseId.Equals(course.CourseId));

            if (enrollment == null)
            {
                return Conflict();
            }

            return Ok();
        }
    }
}
