namespace HRMSPortal.Models
{
    public class SalaryInput
    {
        public int SalaryId { get; set; }

        public int EmployeeId { get; set; }

        public int SalaryAmount { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string CreatedBy { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
