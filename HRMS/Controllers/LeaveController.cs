using HRMS.Models;
using HRMS.Provider;
using HRMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Security.Claims;



namespace HRMS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveProvider _leaveProvider;



        public LeaveController(ILeaveProvider leaveProvider)
        {
            _leaveProvider = leaveProvider;
        }
        [Authorize(Roles = "HR Admin,Employee")]
        [HttpGet]
        public IActionResult GetLeaveDetails()
        {
            try
            {
                var leaveDetails = _leaveProvider.GetLeaveDetails();
                return Ok(leaveDetails);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        [HttpGet("{leaveId}")]
        public IActionResult GetLeaveById(int leaveId)
        {
            try
            {
                var leave = _leaveProvider.GetLeaveById(leaveId);
                if (leave == null)
                    return NotFound();

                return Ok(leave);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }



        }



        [HttpPost]
        public IActionResult AddLeave(Leave leave)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var empId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var leaveId = _leaveProvider.AddLeave(leave, empId);
                return CreatedAtAction(nameof(GetLeaveById), new { leaveId }, leave);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }



        }








        [Authorize(Roles = "HR Admin")]
        [HttpGet("pending")]
        public IActionResult GetPendingLeaves()
        {
            try
            {
                var pendingLeaves = _leaveProvider.GetPendingLeaves();
                return Ok(pendingLeaves);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("LeaveLogAll")]
        public IActionResult GetAllLeaves()
        {
            //var leaves = _leavesProvider.GetAllLeaves();
            //return Ok(leaves);



            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<Leave> leaves = _leaveProvider.GetAllLeaves(userId);
                return Ok(leaves);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpPut("{leaveId}")]
        public IActionResult UpdateLeaveStatus(int leaveId, LeaveUpdateRequest leaveUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();



                var leave = _leaveProvider.GetLeaveById(leaveId);
                if (leave == null)
                    return NotFound();



                if (leaveUpdateRequest.LeaveStatus != leave.LeaveStatus)
                {



                    if (!(leave.LeaveStatus == "Approved" || leave.LeaveStatus == "Rejected"))
                    {
                        // Update the leave status
                        var success = _leaveProvider.UpdateLeaveStatus(leaveId, leaveUpdateRequest.LeaveStatus);
                        if (success)
                            return Ok(success);
                        return StatusCode(500, "Failed to update leave status");
                    }
                    else
                    {
                        return BadRequest("Leave status can only be updated if it is 'Approved' or 'Rejected'");
                    }
                }
                return NoContent();
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }



    public class LeaveUpdateRequest
    {
        public string LeaveStatus { get; set; }
    }
}