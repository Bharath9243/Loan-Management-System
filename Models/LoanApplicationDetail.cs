using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystem.Models
{
    public class LoanApplicationDetail
    {
        public int LoanApplicationDetailId { get; set; }

        [Required]
        public int LoanApplicationId { get; set; }

        [Required]
        public int LoanSchemeId { get; set; }

        [Range(1, 480, ErrorMessage = "Tenure must be between 1 and 480 months.")]
        public int TenureMonths { get; set; }

        public decimal InterestRate { get; set; }

        // Display Only
        public string SchemeName { get; set; } = string.Empty;
    }
}