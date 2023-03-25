using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
	public class EnrollmentRepository : IEnrollmentRepo
	{
		public Enrollment AddEnrollment(Enrollment enrollment) => EnrollmentManagement.Instance.AddEnrollment(enrollment);
		public Enrollment DeleteEnrollment(Enrollment enrollment) => EnrollmentManagement.Instance.DeleteEnrollment(enrollment);
        public Enrollment GetEnrollmentById(int? id) => EnrollmentManagement.Instance.GetEnrollmentById(id);
		public IEnumerable<Enrollment> GetEnrollments() => EnrollmentManagement.Instance.GetEnrollments();
        public IEnumerable<Enrollment> GetEnrollmentsByCourseId(string? courseId) => EnrollmentManagement.Instance.GetEnrollmentsByCourseId(courseId);
        public IEnumerable<Enrollment> GetEnrollmentsByStudentId(int? studentId) => EnrollmentManagement.Instance.GetEnrollmentsByStudentId(studentId);
    }
}
