﻿using HRMS.Models;
using HRMSPortal.Models;

namespace HRMS.Provider.Interface
{
    public interface ISalaryProvider
    {
        public Salary CreateSalary(int employeeId, SalaryInput salary, string createdByName);
        public Salary UpdateSalary(int userId, Salary updatedSalary, string createdByName);
        public Salary GetSalaryById(int userId);
        public List<Salary> GetAllSalaries();
        public Salary DeleteEmployeeSalary(int userId);
    }
}
