using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string EmailId { get; set; } = null!;
    [System.Text.Json.Serialization.JsonIgnore]
    public byte[] PasswordHash { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public byte[] PasswordSalt { get; set; }

    public long PhoneNumber { get; set; }

    public DateOnly HireDate { get; set; }

    public int JobId { get; set; }

    public int DepartmentId { get; set; }

    public int? ManagerId { get; set; }
    public string Roles { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedTime { get; set; }

    public string? UpdatedBy { get; set; }


    public bool IsActive { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Department? Department { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Job? Job { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Leave> Leaves { get; set; } = new List<Leave>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<PerformanceReview> PerformanceReviews { get; set; } = new List<PerformanceReview>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<TrackingTraining> TrackingTraining { get; set; } = new List<TrackingTraining>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();
}
