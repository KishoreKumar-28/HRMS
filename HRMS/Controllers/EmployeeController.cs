using HRMS.Models;
using HRMS.Provider;
using HRMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace HRMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //private ILog _logger;
        private readonly IEmployeeProvider _provider;
        public readonly IEmployeeRepository _repo;
        public EmployeeController(IEmployeeProvider provider, IEmployeeRepository repo)
        {
            _provider = provider;
            _repo = repo;
            //_logger = logger;
        }
        // Only HR Admin role can have access to create employee.
        [Authorize(Roles = "HR Admin")]
        // It is checking the logged in user role and check the requirment role is matching or not.
        [HttpPost]
        public ActionResult<Employee> CreateEmployee(CreateEmployee employee)
        {
            try
            {
                var createdByName = User.FindFirstValue(ClaimTypes.Name);
                var createdEmployee = _provider.CreateEmployee(employee,createdByName);
                //_logger.Information("Created Employee by HR Admin");
                return Ok(createdEmployee);
            }
            catch (CustomException ex)
            {
                //_logger.Error($"{ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the employee.");
            }
        }
        // Only HR Admin role can have access to Delete employee.
        [Authorize(Roles = "HR Admin,Admin,Department Head")]
        // It is checking the logged in user role and check the requirmnt role is matching or not.
        [HttpDelete]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            try
            {
                var existingEmployee = _provider.DeleteEmployee(id);
                return Ok(existingEmployee);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the employee.");
            }
        }
        // Only HR Admin role or Manager can have access to create employee.
        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        // It is checking the logged in user role and check the requirmnt role is matching or not.
        [HttpGet]
        public ActionResult<Employee> GetAllEmployees()
        {
            try
            {
                List<Employee> employees = _provider.GetAllEmployees();
                return Ok(employees);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the employee details.");
            }
        }
        // Only HR Admin or manager role can have access to create employee.
        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        // It is checking the logged in user role and check the requirmnt role is matching or not.
        [HttpGet]
        public ActionResult<Employee> GetEmployeeByUsingAnyInputs(int? departmentId, string? deptName, int? managerId, string? job)
        {
            if (departmentId == null&&deptName==null&&managerId==null&&job==null)
            {
                return BadRequest("Input field not to be null here");
            }
            try
            {
                List<Employee> employees = _provider.GetAllEmployeesByDepartmentOrManagerOrJob(departmentId, deptName, managerId, job);
                return Ok(employees);
            }
             catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the employee details.");
            }
        }
        // Only HR Admin role or employeeor dept head can have access to create employee.
        [Authorize(Roles ="HR Admin")]
        //[Authorize(Roles = "HR Admin,Employee,Department Head")]
        // It is checking the logged in user role and check the requirmnt role is matching or not.
        [HttpPut]
        public ActionResult<Employee> UpdateEmployeeDetails(int id, UpdateByEmployee updatedEmployee)
        {
            try
            {
                var updatedBy = User.FindFirstValue(ClaimTypes.Name);
                var updatedDetails = _provider.UpdateEmployeeDetails(id, updatedEmployee, updatedBy);
                return Ok(updatedDetails);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while Updating the employee.");
            }
        }
        [Authorize(Roles ="HR Admin")]
        [HttpGet]
        public ActionResult<Employee> GetActiveEmployees()
        {
            try
            {
                var activeEmployees = _provider.GetActiveEmployees();
                return Ok(activeEmployees);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the active employee.");
            }
        }
        [Authorize(Roles = "HR Admin")]
        [HttpGet]
        public ActionResult<Employee> GetInActiveEmployees()
        {
            try
            {
                var inActiveEmployees = _provider.GetInActiveEmployees();
                return Ok(inActiveEmployees);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the inactive employee.");
            }
        }
        [Authorize(Roles = "HR Admin,Department Head")]
        [HttpGet]
        public ActionResult<Employee> GetYourDepartmentEmployees()
        {
            try
            {
                var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var inActiveEmployees = _provider.GetYourDepartmentEmployees(userId);
                return Ok(inActiveEmployees);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting your department employees.");
            }
        }
    }
}
