using HRMS.Models;
using HRMS.Repository;



namespace HRMS.Provider
{
    public class AttendanceProvider : IAttendanceProviders
    {
        private readonly IAttendanceRepository _attendanceRepository;
        //Injecting the repository class.
        public AttendanceProvider(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }
        public Attendance checkIn(string userId)
        {
            return _attendanceRepository.checkIn(userId);



        }



        public Attendance checkOut(Attendance checkedInAttendance)
        {
            return _attendanceRepository.checkOut(checkedInAttendance);
        }
        public List<Attendance> GetAllAttendances(string userId)
        {
            return _attendanceRepository.GetAllAttendances(userId);
        }
        public Attendance GetAttendanceByEmployeeId(string employeeId)
        {
            return _attendanceRepository.GetAttendanceByEmployeeId(employeeId);
        }
        public List<Attendance> GetAllAttendanceByEmployeeId(string userId)
        {
            return _attendanceRepository.GetAllAttendanceByEmployeeId(userId);
        }
    }
}