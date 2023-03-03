using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class GradeManagement
    {
        private static GradeManagement instance;
        private static readonly object instancelock = new object();

        public GradeManagement() { }

        public static GradeManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new GradeManagement();
                }
                return instance;
            }
        }

        public Grade AddGrade(Grade Grade)
        {
            try
            {
                Grade _Grade = GetGradeById(Grade.GradeId);
                if (_Grade == null)
                {
                    var context = new AppDBContext();
                    context.Grades.Add(Grade);
                    context.SaveChanges();
                    return Grade;
                }
                else
                {
                    throw new Exception("The Grade's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Grade UpdateGrade(Grade Grade)
        {
            try
            {
                Grade _Grade = GetGradeById(Grade.GradeId);
                if (_Grade != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Grade>(Grade).State = EntityState.Modified;
                    context.SaveChanges();
                    return Grade;
                }
                else
                {
                    throw new Exception("The Grade is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Grade DeleteGrade(Grade Grade)
        {
            try
            {
                Grade _Grade = GetGradeById(Grade.GradeId);
                if (_Grade != null)
                {
                    var context = new AppDBContext();
                    context.Grades.Remove(_Grade);
                    context.SaveChanges();
                    return _Grade;
                }
                else
                {
                    throw new Exception("The Grade is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Grade> GetGrades()
        {
            List<Grade> Grades;
            try
            {
                var context = new AppDBContext();
                Grades = context.Grades.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Grades;
        }

        public Grade GetGradeById(int? id)
        {
            Grade Grade;
            try
            {
                var _dbContext = new AppDBContext();
                Grade = _dbContext.Grades
                    .FirstOrDefault(a => a.GradeId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Grade;
        }
    }
}
