using HRMS.Models;
using HRMS.Provider.Interface;
using HRMS.Repository;
using HRMSPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace YourProject.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryProvider _salaryProvider;

        public SalaryController(ISalaryProvider salaryProvider)
        {
            _salaryProvider = salaryProvider;
        }

        [Authorize(Roles = "HR Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<Salary>> GetAllSalaries()
        {
            try
            {
                var salaries = _salaryProvider.GetAllSalaries();
                return Ok(salaries);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<Salary> GetSalaryById()
        {
            try
            {
                var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var salary = _salaryProvider.GetSalaryById(userId);
                if (salary == null)
                {
                    throw new CustomException("Salary not found.");
                }
                return salary;
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "HR Admin")]
        [HttpPost]
        public ActionResult<Salary> CreateSalary(int employeeId, SalaryInput salary)
        {
            try
            {
                var createdByName = User.FindFirstValue(ClaimTypes.Name);
                var createdSalary = _salaryProvider.CreateSalary(employeeId, salary, createdByName);
                return Created("Post", "Salary Credited");
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "HR Admin")]
        [HttpPut]
        public IActionResult UpdateSalary(int userId, Salary updatedSalary)
        {
            try
            {
                var createdByName = User.FindFirstValue(ClaimTypes.Name);
                var existingSalary = _salaryProvider.UpdateSalary(userId, updatedSalary, createdByName);
                return Ok("Salary updated.");
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "HR Admin")]
        [HttpDelete]
        public IActionResult DeleteEmployeeSalary(int userId)
        {
            try
            {
                var salary = _salaryProvider.DeleteEmployeeSalary(userId);
                if(salary == null)
                {
                    return BadRequest("Employee already deleted");
                }
                return Ok("Employee deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
