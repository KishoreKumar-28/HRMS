using HRMS.Models;



namespace HRMS.Repository
{
    public interface IAttendanceRepository
    {
        Attendance checkIn(string userId);
        Attendance checkOut(Attendance checkedInAttendance);
        List<Attendance> GetAllAttendances(string userId);
        Attendance GetAttendanceByEmployeeId(string employeeId);
        List<Attendance> GetAllAttendanceByEmployeeId(string userId);
    }
}