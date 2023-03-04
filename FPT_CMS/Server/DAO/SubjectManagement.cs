using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class SubjectManagement
    {
        private static SubjectManagement instance;
        private static readonly object instancelock = new object();

        public SubjectManagement() { }

        public static SubjectManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new SubjectManagement();
                }
                return instance;
            }
        }

        public Subject AddSubject(Subject Subject)
        {
            try
            {
                Subject _Subject = GetSubjectById(Subject.SubjectCode);
                if (_Subject == null)
                {
                    var context = new AppDBContext();
                    context.Subjects.Add(Subject);
                    context.SaveChanges();
                    return Subject;
                }
                else
                {
                    throw new Exception("The Subject's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Subject UpdateSubject(Subject Subject)
        {
            try
            {
                Subject _Subject = GetSubjectById(Subject.SubjectCode);
                if (_Subject != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Subject>(Subject).State = EntityState.Modified;
                    context.SaveChanges();
                    return Subject;
                }
                else
                {
                    throw new Exception("The Subject is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Subject DeleteSubject(Subject Subject)
        {
            try
            {
                Subject _Subject = GetSubjectById(Subject.SubjectCode);
                if (_Subject != null)
                {
                    var context = new AppDBContext();
                    context.Subjects.Remove(_Subject);
                    context.SaveChanges();
                    return _Subject;
                }
                else
                {
                    throw new Exception("The Subject is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Subject> GetSubjects()
        {
            List<Subject> Subjects;
            try
            {
                var context = new AppDBContext();
                Subjects = context.Subjects.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Subjects;
        }

        public Subject GetSubjectById(string? id)
        {
            Subject Subject;
            try
            {
                var _dbContext = new AppDBContext();
                Subject = _dbContext.Subjects
                    .FirstOrDefault(a => a.SubjectCode == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Subject;
        }
    }
}
