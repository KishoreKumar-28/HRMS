using HRMS.Models;
using System.Collections.Generic;



namespace HRMS.Provider
{
    public interface ILeaveProvider
    {
        IEnumerable<Leave> GetLeaveDetails();
        Leave GetLeaveById(int leaveId);
        int AddLeave(Leave leave, int empId);
        IEnumerable<Leave> GetPendingLeaves();
        bool UpdateLeaveStatus(int leaveId, string leaveStatus);
        List<Leave> GetAllLeaves(string userId);
    }
}