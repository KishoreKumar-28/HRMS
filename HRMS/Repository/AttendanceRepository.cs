using HRMS.Data;
using HRMS.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Policy;


namespace HRMS.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HrmsdbContext _context;
        public AttendanceRepository(HrmsdbContext context)
        {
            _context = context;
        }

        public Attendance checkIn(string userId)
        {

            var id = int.Parse(userId);
            // Searcing a id in DB.
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                throw new CustomException("Employee not found.");
            }
            // Get the last attendance of the respective employee id in DB.
            var lastAttendance = _context.Attendances
                .Where(a => a.EmployeeId == employee.EmployeeId)
                .OrderByDescending(a => a.CheckInTime)
                .FirstOrDefault();

            // Check if the last attendance is on the same day.
            if (lastAttendance != null && lastAttendance.CheckInTime?.Date == DateTime.Now.Date)
            {
                throw new CustomException("You have already checked in today.");
            }
            // Creating a object for attendance class pass it throw the endpoint.
            var newAttendance = new Attendance
            {
                EmployeeId = employee.EmployeeId,
                CheckInTime = DateTime.Now
            };



            _context.Attendances.Add(newAttendance);
            _context.SaveChanges();



            return newAttendance;

        }

        public Attendance checkOut(Attendance checkedInAttendance)
        {
            /*var id = int.Parse(userId);
                var check = _context.Attendances.FirstOrDefault(c => c.EmployeeId == id);*/
            if (checkedInAttendance != null)
            {
                checkedInAttendance.CheckOutTime = DateTime.Now;
                // Calculate the duration between check-in and check-out times
                TimeSpan? duration = checkedInAttendance.CheckOutTime - checkedInAttendance.CheckInTime;
                // If the duration is more than 9 hours, mark the attendance as present
                if (duration != null && duration.Value.TotalHours > 9)
                {
                    checkedInAttendance.isPresent = true;
                    TimeOnly workingHours = new TimeOnly(duration.Value.Hours, duration.Value.Minutes, duration.Value.Seconds);
                    checkedInAttendance.WorkingHours = workingHours;
                }
                else
                {
                    checkedInAttendance.isPresent = false;
                    TimeOnly workingHours = new TimeOnly(duration.Value.Hours, duration.Value.Minutes, duration.Value.Seconds);
                    checkedInAttendance.WorkingHours = workingHours;
                }



                _context.SaveChanges();
                return checkedInAttendance;
            }
            return null;
        }



        public List<Attendance> GetAllAttendances(string userId)
        {
            try
            {
                var loggedInUserId = int.Parse(userId);
                var loggedInUser = _context.Employees.FirstOrDefault(e => e.EmployeeId == loggedInUserId);



                // Assuming you have a DepartmentId property in the Employee model
                var departmentId = loggedInUser.DepartmentId;



                // Retrieve all attendances for employees in the same department
                var departmentEmployees = _context.Employees
                    .Where(e => e.DepartmentId == departmentId && e.IsActive == true)
                    .ToList();



                var departmentEmployeeIds = departmentEmployees.Select(e => e.EmployeeId);



                return _context.Attendances
                    .Where(a => departmentEmployeeIds.Contains(a.EmployeeId))
                    .ToList();



            }
            catch (Exception ex)
            {
                throw new CustomException("List is Empty");
            }
        }



        public Attendance GetAttendanceByEmployeeId(string employeeId)
        {
            try
            {
                var empId = Int32.Parse(employeeId);

                return _context.Attendances
                    .Where(a => a.EmployeeId == empId)
                    .OrderByDescending(a => a.CheckInTime)
                    .FirstOrDefault();



            }
            catch (Exception ex)
            {
                throw new CustomException("Please check your login.");
            }



        }
        public List<Attendance> GetAllAttendanceByEmployeeId(string userId)
        {
            try
            {
                var id = int.Parse(userId);
                return _context.Attendances
                    .Where(a => a.EmployeeId == id)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException("List is empty");
            }
        }
    }
}