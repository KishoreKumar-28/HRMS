using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }
   
    public DateTime? CheckInTime { get; set; }
    
    public DateTime? CheckOutTime { get; set; }

    public bool? isPresent { get; set; }
    public TimeOnly? WorkingHours { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Employee? Employee { get; set; } = null!;
}
