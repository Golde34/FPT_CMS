using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class CourseManagement
    {
        private static CourseManagement instance;
        private static readonly object instancelock = new object();

        public CourseManagement() { }

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

        public Course AddCourse(Course Course)
        {
            try
            {
                Course _Course = GetCourseById(Course.CourseId);
                if (_Course == null)
                {
                    var context = new AppDBContext();
                    context.Courses.Add(Course);
                    context.SaveChanges();
                    return Course;
                }
                else
                {
                    throw new Exception("The Course's username has already bean taken.");
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
    }
}
