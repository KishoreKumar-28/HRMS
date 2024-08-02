using HRMS.Models;
using HRMS.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace HRMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IEmployeeProvider _provider;
        public JobController(IEmployeeProvider provider)
        {
            _provider = provider;
        }
        // Only HR Admin role can have access to create Job.
        [Authorize(Roles = "HR Admin,Admin")]
        // It is checking the logged in user role and check the requirment role is matching or not.
        [HttpPost("CreateJob")]
        public ActionResult<Job> CreateJob(Job job)
        {
            var createdJob = _provider.CreateJob(job);
            return Created("post", createdJob);
        }
        [HttpGet("GetAllJobs")]
        public ActionResult<Job> GetAllJobs()
        {
            List<Job> jobs = _provider.GetAllJobs();
            return Ok(jobs);
        }
    }
}
