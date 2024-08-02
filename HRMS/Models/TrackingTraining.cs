using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class TrackingTraining
{
    public int TrackingId { get; set; }
    public int EmployeeId { get; set; }

    public int TrainingId { get; set; }

    public bool IsRegister { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Employee? Employee { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Training? Training { get; set; }
}
