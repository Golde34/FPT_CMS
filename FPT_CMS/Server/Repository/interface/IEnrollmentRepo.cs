using Server.Entity;

namespace Server.Repository.@interface
{
	public interface IEnrollmentRepo
	{
        IEnumerable<Enrollment> GetEnrollments();
        Enrollment GetEnrollmentById(int? id);
        IEnumerable<Enrollment> GetEnrollmentsByStudentId(int? studentId);
        IEnumerable<Enrollment> GetEnrollmentsByCourseId(string? courseId);
        Enrollment AddEnrollment(Enrollment enrollment);
        Enrollment DeleteEnrollment(Enrollment enrollment);
    }
}
