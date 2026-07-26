using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystem.Models
{
    public class LoanScheme
    {
        public int LoanSchemeId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string SchemeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "Cost Price must be greater than 0")]
        public decimal ProcessingFee { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Selling Price must be greater than 0")]
        public decimal InterestRate { get; set; }

        public decimal Profit => InterestRate - ProcessingFee;

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int MaximumLoanAmount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int LoanTypeId { get; set; }

        public string LoanTypeName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}