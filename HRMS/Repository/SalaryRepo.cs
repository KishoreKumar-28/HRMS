using HRMS.Data;
using HRMS.Models;
using HRMS.Provider.Interface;
using HRMS.Repository.Interface;
using HRMSPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Security.Claims;

namespace HRMS.Repository.Implementation
{
    public class SalaryRepoClass : ISalaryRepo
    {
        private readonly HrmsdbContext _hrmsdbContext;

        public SalaryRepoClass(HrmsdbContext hrmsdbContext)
        {
            _hrmsdbContext = hrmsdbContext;
        }

        public Salary CreateSalary(int employeeId, SalaryInput salary, string createdByName)
        {
            var existingEmployee = _hrmsdbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId && e.IsActive == true);
            if (existingEmployee == null)
            {
                throw new CustomException("Employee not found.");
            }

            var existingSalary = _hrmsdbContext.Salaries.FirstOrDefault(s => s.EmployeeId == employeeId && s.IsActive == true);
            if (existingSalary != null)
            {
                existingSalary.IsActive = false;
                throw new CustomException("Salary already credited.");
            }

            DateTime today = DateTime.Today;
            DateTime monthStart = new DateTime(today.Year, today.Month, 1);
            DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);

            var leaves = _hrmsdbContext.Leaves
                   .Where(l => l.EmployeeId == employeeId && l.StartDate >= monthStart && l.EndDate <= monthEnd && l.LeaveStatus == "Approved").ToList(); 
            int leavesTaken = leaves.Sum(l => l.NumberOfLeaves);
            int deductionAmount = 0;
            if (leavesTaken > 2)
            {
                int extraLeaves = leavesTaken - 2;
                deductionAmount = extraLeaves * 500;
            }

            Salary newSalary = new Salary();
            newSalary.EmployeeId = salary.EmployeeId;
            newSalary.SalaryAmount = salary.SalaryAmount - deductionAmount; 
            newSalary.EffectiveDate = DateTime.Now;
            newSalary.CreatedBy = createdByName;
            newSalary.IsActive = true;

            _hrmsdbContext.Salaries.Add(newSalary);
            _hrmsdbContext.SaveChanges();

            return newSalary;
        }

        public List<Salary> GetAllSalaries()
        {
            var list = _hrmsdbContext.Salaries.Where(l => l.IsActive == true).ToList();
            return list;
        }

        public Salary GetSalaryById(int userId)
        {
            var employee = _hrmsdbContext.Salaries.Find(userId);
            if (employee == null)
            {
                throw new CustomException("Employee not found.");
            }

            //var salary = _hrmsdbContext.Salaries.FirstOrDefault(s => s.EmployeeId == userId && s.IsActive == true);
            var salary = _hrmsdbContext.Salaries.Where(s=>s.EmployeeId== userId && s.IsActive == true).OrderByDescending(a=>a.SalaryAmount).LastOrDefault();
            if (salary == null)
            {
                salary = new Salary();
                salary.EmployeeId = userId;
            }

            return salary;
        }

        public Salary UpdateSalary(int userId, Salary updatedSalary, string createdByName)
        {
            var employee = _hrmsdbContext.Employees.Find(userId);
            if (employee == null)
            {
                throw new CustomException("Employee not found.");
            }

            var existingSalary = _hrmsdbContext.Salaries
                .Where(s => s.EmployeeId == userId && s.IsActive)
                .OrderByDescending(s => s.SalaryId)
                .FirstOrDefault();

            if (existingSalary == null)
            {
                throw new CustomException("Salary record not found.");
            }

            existingSalary.IsActive = false;

            Salary salary = new Salary();
            salary.EmployeeId = updatedSalary.EmployeeId;
            salary.SalaryAmount = updatedSalary.SalaryAmount;
            salary.EffectiveDate = DateTime.Now; 
            salary.CreatedBy = createdByName; 
            salary.IsActive = true;

            _hrmsdbContext.Salaries.Add(salary);
            _hrmsdbContext.SaveChanges();

            return salary;
        }

        public Salary DeleteEmployeeSalary(int userId)
        {
            var employee = _hrmsdbContext.Employees.Find(userId);
            if (employee == null)
            {
                throw new CustomException("Employee not found.");
            }

            var salary = _hrmsdbContext.Salaries.FirstOrDefault(s => s.EmployeeId == userId && s.IsActive == true);
            if (salary != null)
            {
                Salary? salaries = _hrmsdbContext.Salaries.Find(userId);
                if (salary != null)
                {
                    salary.IsActive = false;
                    _hrmsdbContext.SaveChanges();
                    return salaries;
                }
            }
            return null;

        }
    }
}
