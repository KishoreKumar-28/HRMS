using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Job
{
    public int JobId { get; set; }

    public string JobTitle { get; set; } = null!;

    public int DepartmentId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
