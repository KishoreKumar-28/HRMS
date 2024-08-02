using HRMS.Models;



namespace HRMS.Provider
{

    public interface IAttendanceProviders
    {
        Attendance checkIn(string userId);
        Attendance checkOut(Attendance checkedInAttendance);
        List<Attendance> GetAllAttendances(string userId);
        Attendance GetAttendanceByEmployeeId(string employeeId);
        List<Attendance> GetAllAttendanceByEmployeeId(string userId);
    }
}