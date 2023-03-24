using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class StudentManagement
    {
        private static StudentManagement instance;
        private static readonly object instancelock = new object();

        public StudentManagement() { }

        public static StudentManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new StudentManagement();
                }
                return instance;
            }
        }

        public Student AddStudent(Student Student)
        {
            try
            {
                Student _Student = GetStudentById(Student.Id);
                if (_Student == null)
                {
                    var context = new AppDBContext();
                    context.Students.Add(Student);
                    context.SaveChanges();
                    return Student;
                }
                else
                {
                    throw new Exception("The Student's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Student UpdateStudent(Student Student)
        {
            try
            {
                Student _Student = GetStudentById(Student.Id);
                if (_Student != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Student>(Student).State = EntityState.Modified;
                    context.SaveChanges();
                    return Student;
                }
                else
                {
                    throw new Exception("The Student is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Student DeleteStudent(Student Student)
        {
            try
            {
                Student _Student = GetStudentById(Student.Id);
                if (_Student != null)
                {
                    var context = new AppDBContext();
                    context.Students.Remove(_Student);
                    context.SaveChanges();
                    return _Student;
                }
                else
                {
                    throw new Exception("The Student is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Student> GetStudents()
        {
            List<Student> Students;
            try
            {
                var context = new AppDBContext();
                Students = context.Students.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Students;
        }

        public Student GetStudentById(int? id)
        {
            Student Student;
            try
            {
                var _dbContext = new AppDBContext();
                Student = _dbContext.Students
                    .FirstOrDefault(a => a.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Student;
        }

        public Student GetStudentByAccountId(string id)
        {
            Student Student;
            try
            {
                var _dbContext = new AppDBContext();
                Student = _dbContext.Students
                    .FirstOrDefault(a => a.AccountId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Student;
        }
    }
}
