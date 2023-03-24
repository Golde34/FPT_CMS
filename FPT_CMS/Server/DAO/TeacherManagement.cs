using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class TeacherManagement
    {
        private static TeacherManagement instance;
        private static readonly object instancelock = new object();

        public TeacherManagement() { }

        public static TeacherManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new TeacherManagement();
                }
                return instance;
            }
        }

        public Teacher AddTeacher(Teacher Teacher)
        {
            try
            {
                Teacher _Teacher = GetTeacherById(Teacher.Id);
                if (_Teacher == null)
                {
                    var context = new AppDBContext();
                    context.Teachers.Add(Teacher);
                    context.SaveChanges();
                    return Teacher;
                }
                else
                {
                    throw new Exception("The Teacher's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Teacher UpdateTeacher(Teacher Teacher)
        {
            try
            {
                Teacher _Teacher = GetTeacherById(Teacher.Id);
                if (_Teacher != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Teacher>(Teacher).State = EntityState.Modified;
                    context.SaveChanges();
                    return Teacher;
                }
                else
                {
                    throw new Exception("The Teacher is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Teacher DeleteTeacher(Teacher Teacher)
        {
            try
            {
                Teacher _Teacher = GetTeacherById(Teacher.Id);
                if (_Teacher != null)
                {
                    var context = new AppDBContext();
                    context.Teachers.Remove(_Teacher);
                    context.SaveChanges();
                    return _Teacher;
                }
                else
                {
                    throw new Exception("The Teacher is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            List<Teacher> Teachers;
            try
            {
                var context = new AppDBContext();
                Teachers = context.Teachers.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Teachers;
        }

        public Teacher GetTeacherById(string? id)
        {
            Teacher Teacher;
            try
            {
                var _dbContext = new AppDBContext();
                Teacher = _dbContext.Teachers
                    .FirstOrDefault(a => a.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Teacher;
        }

        public Teacher GetTeacherByAccountId(string id)
        {
            Teacher teacher;
            try
            {
                var _dbContext = new AppDBContext();
                teacher = _dbContext.Teachers
                    .FirstOrDefault(a => a.AccountId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return teacher;
        }
    }
}
