using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystem.Models
{
    public class LoanApplication
    {
        public int LoanApplicationId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a customer")]
        public int UserId { get; set; }

        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Application status is required")]
        public string ApplicationStatus { get; set; } = "Pending";

        [Range(0.01, double.MaxValue, ErrorMessage = "Loan amount must be greater than 0")]
        public decimal LoanAmount { get; set; }

        // Comes from JOIN with Users table
        public string Name { get; set; } = string.Empty;
    }
}