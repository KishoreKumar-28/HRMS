using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string Location { get; set; } = null!;
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Training> Training { get; set; } = new List<Training>();
}
