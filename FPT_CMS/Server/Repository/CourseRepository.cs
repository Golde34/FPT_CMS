using Server.DAO;
using Server.DTO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class CourseRepository : ICourseRepo
    {
        Course ICourseRepo.AddCourse(Course course) => CourseManagement.Instance.AddCourse(course);
        Course ICourseRepo.UpdateCourse(Course Course) => CourseManagement.Instance.UpdateCourse(Course);
        Course ICourseRepo.DeleteCourse(Course Course) => CourseManagement.Instance.DeleteCourse(Course);
        IEnumerable<Course> ICourseRepo.GetCourses() => CourseManagement.Instance.GetCourses();
        Course ICourseRepo.GetCourseById(string? id) => CourseManagement.Instance.GetCourseById(id);
        Course ICourseRepo.GetLastCourse() => CourseManagement.Instance.GetLastCourse();
    }
}