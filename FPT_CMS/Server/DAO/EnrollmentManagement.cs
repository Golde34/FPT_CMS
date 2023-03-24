using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
	public class EnrollmentManagement
	{
        private static EnrollmentManagement instance;
        private static readonly object instancelock = new object();

        public EnrollmentManagement() { }

        public static EnrollmentManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new EnrollmentManagement();
                }
                return instance;
            }
        }

        public IEnumerable<Enrollment> GetEnrollments()
        {
            List<Enrollment> enrollments;
            try
            {
                var context = new AppDBContext();
                enrollments = context.Enrollments.Include(e => e.Course).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return enrollments;
        }

        public Enrollment GetEnrollmentById(int? id)
        {
            Enrollment enrollment;
            try
            {
                var _dbContext = new AppDBContext();
                enrollment = _dbContext.Enrollments.Include(e => e.Course)
                    .FirstOrDefault(e => e.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return enrollment;
        }

        public IEnumerable<Enrollment> GetEnrollmentsByStudentId(int? studentId)
        {
            List<Enrollment> enrollments;
            try
            {
                var _dbContext = new AppDBContext();
                enrollments = _dbContext.Enrollments.Include(e => e.Course)
                    .Where(e => e.StudentId == studentId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return enrollments;
        }

        public IEnumerable<Enrollment> GetEnrollmentsByCourseId(string? courseId)
        {
            List<Enrollment> enrollments;
            try
            {
                var _dbContext = new AppDBContext();
                enrollments = _dbContext.Enrollments.Include(e => e.Course)
                    .Where(e => e.CourseId == courseId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return enrollments;
        }
    }
}
