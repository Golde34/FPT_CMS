using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class DepartmentManagement
    {
        private static DepartmentManagement instance;
        private static readonly object instancelock = new object();

        public DepartmentManagement() { }

        public static DepartmentManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new DepartmentManagement();
                }
                return instance;
            }
        }

        public Department AddDepartment(Department Department)
        {
            try
            {
                Department _Department = GetDepartmentById(Department.DepartmentId);
                if (_Department == null)
                {
                    var context = new AppDBContext();
                    context.Departments.Add(Department);
                    context.SaveChanges();
                    return Department;
                }
                else
                {
                    throw new Exception("The Department's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Department UpdateDepartment(Department Department)
        {
            try
            {
                Department _Department = GetDepartmentById(Department.DepartmentId);
                if (_Department != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Department>(Department).State = EntityState.Modified;
                    context.SaveChanges();
                    return Department;
                }
                else
                {
                    throw new Exception("The Department is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Department DeleteDepartment(Department Department)
        {
            try
            {
                Department _Department = GetDepartmentById(Department.DepartmentId);
                if (_Department != null)
                {
                    var context = new AppDBContext();
                    context.Departments.Remove(_Department);
                    context.SaveChanges();
                    return _Department;
                }
                else
                {
                    throw new Exception("The Department is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Department> GetDepartments()
        {
            List<Department> Departments;
            try
            {
                var context = new AppDBContext();
                Departments = context.Departments.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Departments;
        }

        public Department GetDepartmentById(int? id)
        {
            Department Department;
            try
            {
                var _dbContext = new AppDBContext();
                Department = _dbContext.Departments
                    .FirstOrDefault(a => a.DepartmentId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Department;
        }
    }
}
