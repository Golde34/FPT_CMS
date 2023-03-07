using Server.DTO;
using Server.Entity;

namespace Server.Repository.@interface
{
    public interface ICourseRepo
    {
        Course AddCourse(CourseDTO Course);
        Course UpdateCourse(Course Course);
        Course DeleteCourse(Course Course);
        IEnumerable<Course> GetCourses();
        Course GetCourseById(string? id);
        Course GetLastCourse();
    }
}
