using HRMS.Models;

namespace HRMS.Provider
{
    public interface IEmployeeProvider
    {
        Employee CreateEmployee(CreateEmployee employee,string createdByName);
        Employee GetEmployee(string id);
        List<Employee> GetAllEmployees();
        List<Employee> GetAllEmployeesByDepartmentOrManagerOrJob(int? departmentId, string? deptName, int? managerId, string? job);
        Employee UpdateEmployeeDetails(int id, UpdateByEmployee updatedEmployee,string updatedBy);
        Employee DeleteEmployee(int id);
        Job CreateJob(Job job);
        List<Job> GetAllJobs();
        List<Employee> GetActiveEmployees();
        List<Employee> GetInActiveEmployees();
        List<Employee> GetYourDepartmentEmployees(int userId);
    }
}
