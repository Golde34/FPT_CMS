using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class DepartmentRepository : IDepartmentRepo
    {
        Department IDepartmentRepo.AddDepartment(Department Department) => DepartmentManagement.Instance.AddDepartment(Department);
        Department IDepartmentRepo.UpdateDepartment(Department Department) => DepartmentManagement.Instance.UpdateDepartment(Department);
        Department IDepartmentRepo.DeleteDepartment(Department Department) => DepartmentManagement.Instance.DeleteDepartment(Department);
        IEnumerable<Department> IDepartmentRepo.GetDepartments() => DepartmentManagement.Instance.GetDepartments();
        Department IDepartmentRepo.GetDepartmentById(int? id) => DepartmentManagement.Instance.GetDepartmentById(id);
    }
}
