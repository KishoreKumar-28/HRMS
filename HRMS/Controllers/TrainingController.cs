using HRMS.Models;
using HRMS.Provider;
using HRMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Implentation class of training provider
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingPro _trainingService;

        public TrainingController(ITrainingPro trainingService)
        {
            _trainingService = trainingService;
        }

        // GET: api/ get all training details.
        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        [HttpGet("GetAllTrainingDetailsByHR")]
        public ActionResult<IEnumerable<Training>> GetAllTrainingDetails()
        {
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var trainings = _trainingService.GetAllTrainingDetails(id);
            return Ok(trainings);
        }

        // GET: api/get all upcoming training details.
        [Authorize(Roles = "HR Admin,Admin,Employee,Department Head,Functional Head")]
        [HttpGet("AllUpcomingTrainings")]
        public ActionResult<IEnumerable<Training>> GetAllUpcomingTrainings()
        {

            var trainings = _trainingService.GetAllUpcomingTrainings();
            return Ok(trainings);
        }

        // GET: api/get training details by training id.
        [Authorize(Roles = "HR Admin,Admin,Employee,Department Head,Functional Head ")]
        [HttpGet("GetByTrainingId{id}")]
        public ActionResult<Training> GetTrainingById(int id)
        {
            try
            {
                var training = _trainingService.GetByTrainingId(id);

                if (training == null)
                {
                    return NotFound();
                }

                return Ok(training);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: api/get training details by department id.
        [Authorize(Roles = "HR Admin,Admin,Employee,Department Head,Functional Head ")]
        [HttpGet("GetByDeptId{id}")]
        public ActionResult<Training> GetDeptById(int id)
        {
            try
            {
                var training = _trainingService.GetByDeptId(id);

                if (training == null)
                {
                    return NotFound();
                }

                return Ok(training);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Add training details
        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        [HttpPost("AddTrainingDetails")]
        public IActionResult AddTraining(Training training)
        {
            try
            {
                _trainingService.AddTraining(training);
                return CreatedAtAction(nameof(GetTrainingById), new { id = training.TrainingId }, training);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Update the training details
        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        [HttpPut("UpdateTrainingDetails/{id}")]
        public IActionResult UpdateTraining(int id, Training updatedTraining)
        {
            if (id != updatedTraining.TrainingId)
            {
                return BadRequest();
            }

            _trainingService.UpdateTraining(updatedTraining);
            return NoContent();

        }
        // DELETE: api/SoftDelete the training
        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        [HttpDelete("DeleteTrainingDetails/{id}")]
        public ActionResult DeleteTraining(int id)
        {
            var existingTraining = _trainingService.GetByTrainingId(id);
            if (existingTraining == null)
            {
                return NotFound();
            }
            _trainingService.DeleteTraining(id);
            return Ok();
        }
    }
}
