using HRMS.Models;
using HRMS.Repository;

namespace HRMS.Provider
{
    public class EmployeeProvider : IEmployeeProvider
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeProvider(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public Employee CreateEmployee(CreateEmployee employee, string createdByName)
        {
            return _repository.CreateEmployee(employee,createdByName);
        }

        public Job CreateJob(Job job)
        {
            return _repository.CreateJob(job);
        }

  /*      public string Encrypt(string plainText)
        {
            return _repository.Encrypt(plainText);
        }*/

        public Employee DeleteEmployee(int id)
        {
            return _repository.DeleteEmployee(id);
        }

        public List<Employee> GetActiveEmployees()
        {
            return _repository.GetActiveEmployees();
        }

        public List<Employee> GetAllEmployees()
        {
            return _repository.GetAllEmployees();
        }
        public List<Employee> GetAllEmployeesByDepartmentOrManagerOrJob(int? departmentId, string? deptName, int? managerId, string? job)
        {
            return _repository.GetAllEmployeesByDepartmentOrManagerOrJob(departmentId, deptName, managerId, job);
        }

        public List<Job> GetAllJobs()
        {
            return _repository.GetAllJobs();
        }

        public Employee GetEmployee(string userId)
        {
            return _repository.GetEmployee(userId);
        }

        public List<Employee> GetInActiveEmployees()
        {
            return _repository.GetInActiveEmployees();
        }

        public List<Employee> GetYourDepartmentEmployees(int userId)
        {
            return _repository.GetYourDepartmentEmployees(userId);
        }

        public Employee UpdateEmployeeDetails(int id, UpdateByEmployee updatedEmployee, string updatedBy)
        {
            return _repository.UpdateEmployeeDetails(id, updatedEmployee,updatedBy);
        }

     /*   public string Decrypt(string ciphen)
        {
            return _repository.Decrypt(ciphen);
        }*/
    }
}
