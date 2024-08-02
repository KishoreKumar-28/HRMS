using HRMS.Models;
using HRMS.Provider;

namespace HRMS.Repository
{
    public interface IEmployeeRepository
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

        void CreatePasswordhash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordhash(string password, byte[] passwordHash, byte[] passwordSalt);

    }
}
