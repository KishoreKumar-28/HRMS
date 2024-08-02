using HRMS.Models;
using HRMS.Repository;
using System.Collections.Generic;



namespace HRMS.Provider
{
    public class LeaveProvider : ILeaveProvider
    {
        private readonly ILeaveRepository _leaveRepository;



        public LeaveProvider(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }



        public IEnumerable<Leave> GetLeaveDetails()
        {
            return _leaveRepository.GetLeaveDetails();
        }



        public Leave GetLeaveById(int leaveId)
        {
            return _leaveRepository.GetLeaveById(leaveId);
        }



        public int AddLeave(Leave leave, int empId)
        {
            return _leaveRepository.AddLeave(leave, empId);
        }



        public IEnumerable<Leave> GetPendingLeaves()
        {
            return _leaveRepository.GetPendingLeaves();
        }



        public bool UpdateLeaveStatus(int leaveId, string leaveStatus)
        {
            return _leaveRepository.UpdateLeaveStatus(leaveId, leaveStatus);
        }



        public List<Leave> GetAllLeaves(string userId)
        {
            return _leaveRepository.GetAllLeaves(userId);
        }
    }
}