using HRMS.Data;
using HRMS.Models;

namespace HRMS.Repository
{
    public interface IDepartmentRepository
    {
        Department AddDepartment(Department department);
        Department UpdateDepartment(UpdateDepartment department, int deptId);
        List<Department> GetDepartment(int? deptId, string? deptName, string? location);
        List<Department> GetAllDepartment();


    }
}
