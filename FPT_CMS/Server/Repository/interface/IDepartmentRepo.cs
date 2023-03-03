using Server.Entity;

namespace Server.Repository.@interface
{
    public interface IDepartmentRepo
    {
        Department AddDepartment(Department Department);
        Department UpdateDepartment(Department Department);
        Department DeleteDepartment(Department Department);
        IEnumerable<Department> GetDepartments();
        Department GetDepartmentById(int? id);
    }
}
