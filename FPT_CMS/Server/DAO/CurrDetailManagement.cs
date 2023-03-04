using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class CurrDetailManagement
    {
        private static CurrDetailManagement instance;
        private static readonly object instancelock = new object();

        public CurrDetailManagement() { }

        public static CurrDetailManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new CurrDetailManagement();
                }
                return instance;
            }
        }

        public CurriculumDetail AddCurriculumDetail(CurriculumDetail CurriculumDetail)
        {
            try
            {
                CurriculumDetail _CurriculumDetail = GetCurriculumDetailById(CurriculumDetail.CurriculumId);
                if (_CurriculumDetail == null)
                {
                    var context = new AppDBContext();
                    context.CurriculumDetails.Add(CurriculumDetail);
                    context.SaveChanges();
                    return CurriculumDetail;
                }
                else
                {
                    throw new Exception("The CurriculumDetail's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CurriculumDetail UpdateCurriculumDetail(CurriculumDetail CurriculumDetail)
        {
            try
            {
                CurriculumDetail _CurriculumDetail = GetCurriculumDetailById(CurriculumDetail.CurriculumId);
                if (_CurriculumDetail != null)
                {
                    var context = new AppDBContext();
                    context.Entry<CurriculumDetail>(CurriculumDetail).State = EntityState.Modified;
                    context.SaveChanges();
                    return CurriculumDetail;
                }
                else
                {
                    throw new Exception("The CurriculumDetail is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CurriculumDetail DeleteCurriculumDetail(CurriculumDetail CurriculumDetail)
        {
            try
            {
                CurriculumDetail _CurriculumDetail = GetCurriculumDetailById(CurriculumDetail.CurriculumId);
                if (_CurriculumDetail != null)
                {
                    var context = new AppDBContext();
                    context.CurriculumDetails.Remove(_CurriculumDetail);
                    context.SaveChanges();
                    return _CurriculumDetail;
                }
                else
                {
                    throw new Exception("The CurriculumDetail is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CurriculumDetail> GetCurriculumDetails()
        {
            List<CurriculumDetail> CurriculumDetails;
            try
            {
                var context = new AppDBContext();
                CurriculumDetails = context.CurriculumDetails.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return CurriculumDetails;
        }

        public CurriculumDetail GetCurriculumDetailById(int? id)
        {
            CurriculumDetail CurriculumDetail;
            try
            {
                var _dbContext = new AppDBContext();
                CurriculumDetail = _dbContext.CurriculumDetails
                    .FirstOrDefault(a => a.CurriculumId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return CurriculumDetail;
        }
    }
}
