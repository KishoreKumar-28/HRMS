using HRMS.Models;
using HRMS.Provider;
using HRMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentProvider _provider;
        public DepartmentController(IDepartmentProvider provider)
        {
            _provider = provider;
        }
        // Only HR Admin and Admin have access to create department.
        [Authorize(Roles = "HR Admin,Admin")]
        [HttpPost]
        public ActionResult<Department> CreateDepartment(Department department)
        {
            // It is checking the logged in user role and check the requirmnt role is matching or not.
            try
            {
                var newDepartment = _provider.AddDepartment(department);
                return Created("post", newDepartment);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the Department.");
            }
        }
        // Only HR Admin and Admin have access to Update department.
        [Authorize(Roles = "HR Admin,Admin")]
        [HttpPut]
        public ActionResult<Department> UpdateDepartmentDetails(UpdateDepartment department, int deptId)
        {
            try
            {
                var updatedDept = _provider.UpdateDepartment(department, deptId);
                return Ok(updatedDept);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the department.");
            }
        }
        [HttpGet]
        public ActionResult<Department> GetDepartmentBySearch(int? deptId, string? deptName, string? location)
        {
            try
            {
                List<Department> department = _provider.GetDepartment(deptId, deptName, location);
                return Ok(department);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the department.");
            }
        }
        [HttpGet]
        public ActionResult<Department> GetAllDepartment()
        {
            try
            {
                return Ok(_provider.GetAllDepartment());
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the department.");
            }
        }
    }
}
