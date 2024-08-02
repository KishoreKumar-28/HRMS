using HRMS.Models;
using HRMS.Provider;
using HRMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;



namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceProviders _attendanceProvider;

        public AttendanceController(IAttendanceProviders attendanceProvider)
        {
            _attendanceProvider = attendanceProvider;
        }
        [HttpPost("checkin")]
        public IActionResult CheckIn()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var checkedInAttendance = _attendanceProvider.checkIn(userId);
                if (checkedInAttendance != null)
                {
                    return Ok(checkedInAttendance);
                }
                else
                {
                    return BadRequest("Check-in failed.");
                }
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No employeee found.");
            }
        }



        [HttpPost("checkout")]
        public IActionResult CheckOut()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var checkedInAttendance = _attendanceProvider.GetAttendanceByEmployeeId(userId);
                if (checkedInAttendance != null && checkedInAttendance.CheckOutTime == null)
                {
                    //Attendance attendance = new Attendance();
                    var checkedOutAttendance = _attendanceProvider.checkOut(checkedInAttendance);
                    if (checkedOutAttendance != null)
                    {
                        return Ok(checkedOutAttendance);
                    }
                    else
                    {
                        return BadRequest("Check-out failed.");
                    }
                }
                else if (checkedInAttendance == null)
                {
                    return BadRequest("You are unable to check out until you have checked in.");
                }
                else
                {
                    return BadRequest("Your are already checkout today");
                }
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No employeee found.");
            }
        }
        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        [HttpGet("AttendanceLogAll")]
        public IActionResult GetAllAttendances()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<Attendance> attendances = _attendanceProvider.GetAllAttendances(userId);
                return Ok(attendances);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No employeee found.");
            }





        }
        [HttpGet("AttendanceLog")]
        public IActionResult GetAttendanceById()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var attendances = _attendanceProvider.GetAllAttendanceByEmployeeId(userId);



                if (attendances == null)
                {
                    return NotFound("No attendance records found for the specified employee ID.");
                }



                return Ok(attendances);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No employeee found");
            }



        }
    }
}