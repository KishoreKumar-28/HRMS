namespace HRMS.Models
{
    public class CreateEmployee
    {
        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string EmailId { get; set; } = null!;

        public string Password { get; set; } = null!;

        public long PhoneNumber { get; set; }

        public DateOnly HireDate { get; set; }

        public int JobId { get; set; }

        public int DepartmentId { get; set; }

        public int? ManagerId { get; set; }
        public string Roles { get; set; }

    }
}
