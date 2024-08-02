using HRMS.Repository;
using HRMS.Models;
using HRMS.Provider.Interface;
using HRMS.Repository.Interface;
using HRMSPortal.Models;

namespace HRMS.Provider.Implementation
{
    public class SalaryProviderClass : ISalaryProvider
    {
        private readonly ISalaryRepo _salaryRepo;
        public SalaryProviderClass(ISalaryRepo salaryRepo)
        {
            _salaryRepo = salaryRepo;
        }

        public Salary CreateSalary(int employeeId, SalaryInput salary, string createdByName)
        {
            return _salaryRepo.CreateSalary(employeeId, salary, createdByName);
        }

        public List<Salary> GetAllSalaries()
        {
            return _salaryRepo.GetAllSalaries();
        }

        public Salary GetSalaryById(int userId)
        {
            return _salaryRepo.GetSalaryById(userId);
        }

        public Salary UpdateSalary(int userId, Salary updatedSalary, string createdByName)
        {
            return _salaryRepo.UpdateSalary(userId, updatedSalary, createdByName);
        }

        public Salary DeleteEmployeeSalary(int userId)
        {
            return _salaryRepo.DeleteEmployeeSalary(userId);
        }
    }
}
