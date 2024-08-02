using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class LeaveType
{
    public int LeaveTypeId { get; set; }

    public string LeaveTypes { get; set; } = null!;

    public int? AvailableBalance { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedTime { get; set; }

    public bool IsActive { get; set; }
}
