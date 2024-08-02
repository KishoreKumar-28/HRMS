using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Training
{
    public int TrainingId { get; set; }

    public int DepartmentId { get; set; }

    public string TrainingName { get; set; } = null!;

    public string Trainer { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedTime { get; set; }
    public bool IsActive { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Employee? Employee { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Department? Department { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<TrackingTraining> TrackingTrainings { get; set; } = new List<TrackingTraining>();
}
