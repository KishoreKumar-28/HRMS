using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Leave
{
    public int LeaveId { get; set; }

    public int EmployeeId { get; set; }

    public string LeaveType { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string LeaveStatus { get; set; } = null!;

    public string LeaveReason { get; set; } = null!;

    public int NumberOfLeaves { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Employee? Employee { get; set; }
}
