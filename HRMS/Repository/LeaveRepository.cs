using HRMS.Models;
using HRMS.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;



namespace HRMS.Repository
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly HrmsdbContext _dbContext;



        public LeaveRepository(HrmsdbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public IEnumerable<Leave> GetLeaveDetails()
        {
            try
            {
                return _dbContext.Leaves.ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException("Bad Request");
            }

        }



        public Leave GetLeaveById(int leaveId)
        {
            try
            {
                return _dbContext.Leaves.FirstOrDefault(l => l.LeaveId == leaveId);
            }
            catch (Exception ex)
            {
                throw new CustomException("Invalid LeaveId");
            }






        }



        public int AddLeave(Leave leave, int empId)
        {
            try
            {
                leave.EmployeeId = empId;
                leave.LeaveStatus = "Pending";
                leave.CreatedTime = DateTime.Now;
                leave.UpdatedTime = null;
                _dbContext.Leaves.Add(leave);
                _dbContext.SaveChanges();
                return leave.LeaveId;
            }
            catch (Exception ex)
            {
                throw new CustomException("Invalid Inputs");
            }

        }



        public IEnumerable<Leave> GetPendingLeaves()
        {
            try
            {
                return _dbContext.Leaves.Where(l => l.LeaveStatus == "Pending").ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException("Unable to get Pending Leaves");
            }

        }



        public bool UpdateLeaveStatus(int leaveId, string leaveStatus)
        {
            try
            {
                var leave = _dbContext.Leaves.FirstOrDefault(l => l.LeaveId == leaveId);
                if (leave != null)
                {
                    leave.LeaveStatus = leaveStatus;
                    leave.UpdatedTime = DateTime.Now;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new CustomException("Unable To Update the Status");
            }



        }
        public List<Leave> GetAllLeaves(string userId)
        {
            try
            {
                var id = int.Parse(userId);
                return _dbContext.Leaves
                    .Where(l => l.EmployeeId == id)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException("List is empty");
            }
        }
    }
}