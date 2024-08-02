using HRMS.Models;

namespace HRMS.Provider
{
    public interface IDepartmentProvider
    {
        Department AddDepartment(Department department);
        Department UpdateDepartment(UpdateDepartment department, int deptId);
        List<Department> GetDepartment(int? deptId, string? deptName, string? location);
        List<Department> GetAllDepartment();
    }
}
