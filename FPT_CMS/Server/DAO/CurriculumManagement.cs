using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class CurriculumManagement
    {
        private static CurriculumManagement instance;
        private static readonly object instancelock = new object();

        public CurriculumManagement() { }

        public static CurriculumManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new CurriculumManagement();
                }
                return instance;
            }
        }

        public Curriculum AddCurriculum(Curriculum Curriculum)
        {
            try
            {
                Curriculum _Curriculum = GetCurriculumById(Curriculum.CurriculumId);
                if (_Curriculum == null)
                {
                    var context = new AppDBContext();
                    context.Curricula.Add(Curriculum);
                    context.SaveChanges();
                    return Curriculum;
                }
                else
                {
                    throw new Exception("The Curriculum's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Curriculum UpdateCurriculum(Curriculum Curriculum)
        {
            try
            {
                Curriculum _Curriculum = GetCurriculumById(Curriculum.CurriculumId);
                if (_Curriculum != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Curriculum>(Curriculum).State = EntityState.Modified;
                    context.SaveChanges();
                    return Curriculum;
                }
                else
                {
                    throw new Exception("The Curriculum is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Curriculum DeleteCurriculum(Curriculum Curriculum)
        {
            try
            {
                Curriculum _Curriculum = GetCurriculumById(Curriculum.CurriculumId);
                if (_Curriculum != null)
                {
                    var context = new AppDBContext();
                    context.Curricula.Remove(_Curriculum);
                    context.SaveChanges();
                    return _Curriculum;
                }
                else
                {
                    throw new Exception("The Curriculum is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Curriculum> GetCurriculums()
        {
            List<Curriculum> Curriculums;
            try
            {
                var context = new AppDBContext();
                Curriculums = context.Curricula.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Curriculums;
        }

        public Curriculum GetCurriculumById(int? id)
        {
            Curriculum Curriculum;
            try
            {
                var _dbContext = new AppDBContext();
                Curriculum = _dbContext.Curricula
                    .FirstOrDefault(a => a.CurriculumId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Curriculum;
        }
    }
}
