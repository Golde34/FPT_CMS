using Microsoft.EntityFrameworkCore;
using Server.DTO;
using Server.Entity;

namespace Server.DAO
{
    public class CourseManagement
    {
        private static CourseManagement instance;
        private static readonly object instancelock = new object();

        public CourseManagement()
        {
        }

        public static CourseManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new CourseManagement();
                }

                return instance;
            }
        }

        public Course AddCourse(CourseDTO course)
        {
            try
            {
                Course _Course = new Course
                {
                    CourseId = course.CourseID,
                    CourseName = course.CourseName,
                    Slot = Convert.ToInt32(course.Slot),
                    SemesterId = course.Semester,
                    SubjectCode = course.Subject
                };
                Course _course = GetCourseById(_Course.CourseId);
                if (_course == null)
                {
                    var context = new AppDBContext();
                    context.Courses.Add(_Course);
                    context.SaveChanges();
                    return _course;
                }
                else
                {
                    throw new Exception("The Course's username has already been taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Course UpdateCourse(Course Course)
        {
            try
            {
                Course _Course = GetCourseById(Course.CourseId);
                if (_Course != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Course>(Course).State = EntityState.Modified;
                    context.SaveChanges();
                    return Course;
                }
                else
                {
                    throw new Exception("The Course is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Course DeleteCourse(Course Course)
        {
            try
            {
                Course _Course = GetCourseById(Course.CourseId);
                if (_Course != null)
                {
                    var context = new AppDBContext();
                    context.Courses.Remove(_Course);
                    context.SaveChanges();
                    return _Course;
                }
                else
                {
                    throw new Exception("The Course is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Course> GetCourses()
        {
            List<Course> Courses;
            try
            {
                var context = new AppDBContext();
                Courses = context.Courses.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Courses;
        }

        public Course GetCourseById(string? id)
        {
            Course Course;
            try
            {
                var _dbContext = new AppDBContext();
                Course = _dbContext.Courses
                    .FirstOrDefault(a => a.CourseId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Course;
        }

        public Course GetLastCourse()
        {
            Course course;
            try
            {
                var _dbContext = new AppDBContext();
                course = _dbContext.Courses.OrderBy(p => Convert.ToInt32(p.CourseId)).Last();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return course;
        }
    }
}