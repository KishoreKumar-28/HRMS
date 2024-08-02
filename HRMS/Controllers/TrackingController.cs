using HRMS.Models;
using HRMS.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Implementation class for Tracking provider.
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingPro _provider;

        public TrackingController(ITrackingPro pro)
        {
            _provider = pro;
        }

        // GET: api/Get all training employees
        [Authorize(Roles = "HR Admin,Admin,Employee,Department Head,Functional Head")]
        [HttpGet("GetAllTrainingEmployees")]
        public IActionResult GetAll()
        {
            var trackingTrainings = _provider.GetAll();
            return Ok(trackingTrainings);
        }
        // GET: api/ Get registered trainings employees
        [Authorize(Roles = "HR Admin,Admin,Employee,Department Head,Functional Head")]
        [HttpGet("GetByEmpId")]
        public IActionResult GetByEmpId(int id)
        {
          
            var trackingTraining = _provider.GetByEmpId(id);
            if (trackingTraining == null)
                return NotFound();

            return Ok(trackingTraining);
        }
        // Enroll for training
        [Authorize(Roles = "HR Admin,Admin,Employee,Department Head,Functional Head")]
        [HttpPost("EnrollTraining")]
        public IActionResult Add([FromBody] TrackingTraining track)
        {
            try
            {
                _provider.Add(track);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update the registered training
        [Authorize(Roles = "HR Admin,Admin,Employee,Department Head,Functional Head")]
        [HttpPut("UpdateTraining/{Empid}")]
        public IActionResult Update(int id,[FromBody] TrackingTraining track)
        {
            var existingTrack = _provider.GetByEmpId(id);
            if (existingTrack == null)
                return NotFound();

            existingTrack.EmployeeId = track.EmployeeId;

            _provider.Update(existingTrack);
            return Ok();
        }
    }
}

