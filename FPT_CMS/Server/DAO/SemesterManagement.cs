using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class SemesterManagement
    {
        private static SemesterManagement instance;
        private static readonly object instancelock = new object();

        public SemesterManagement() { }

        public static SemesterManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new SemesterManagement();
                }
                return instance;
            }
        }

        public Semester AddSemester(Semester Semester)
        {
            try
            {
                Semester _Semester = GetSemesterById(Semester.SemesterId);
                if (_Semester == null)
                {
                    var context = new AppDBContext();
                    context.Semesters.Add(Semester);
                    context.SaveChanges();
                    return Semester;
                }
                else
                {
                    throw new Exception("The Semester's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Semester UpdateSemester(Semester Semester)
        {
            try
            {
                Semester _Semester = GetSemesterById(Semester.SemesterId);
                if (_Semester != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Semester>(Semester).State = EntityState.Modified;
                    context.SaveChanges();
                    return Semester;
                }
                else
                {
                    throw new Exception("The Semester is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Semester DeleteSemester(Semester Semester)
        {
            try
            {
                Semester _Semester = GetSemesterById(Semester.SemesterId);
                if (_Semester != null)
                {
                    var context = new AppDBContext();
                    context.Semesters.Remove(_Semester);
                    context.SaveChanges();
                    return _Semester;
                }
                else
                {
                    throw new Exception("The Semester is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Semester> GetSemesters()
        {
            List<Semester> Semesters;
            try
            {
                var context = new AppDBContext();
                Semesters = context.Semesters.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Semesters;
        }

        public Semester GetSemesterById(string? id)
        {
            Semester Semester;
            try
            {
                var _dbContext = new AppDBContext();
                Semester = _dbContext.Semesters
                    .FirstOrDefault(a => a.SemesterId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Semester;
        }
    }
}
