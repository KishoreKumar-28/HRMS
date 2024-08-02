using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class PerformanceReview
{
    public int ReviewId { get; set; }

    public int EmployeeId { get; set; }

    public int ReviewerId { get; set; }

    public DateTime ReviewDate { get; set; }
    
    public string? Comments { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedTime { get; set; }

    public bool IsActive { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Employee? Employee { get; set; }
}
