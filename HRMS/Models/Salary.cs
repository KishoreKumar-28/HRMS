using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Salary
{
    public int SalaryId { get; set; }

    public int EmployeeId { get; set; }

    public int SalaryAmount { get; set; }

    public DateTime EffectiveDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsActive { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Employee? Employee { get; set; }
}
