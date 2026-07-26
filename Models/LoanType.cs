using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystem.Models
{
    public class LoanType
    {
        public int LoanTypeId { get; set; }

        [Required(ErrorMessage = "Loan Type Name is required")]
        public string LoanTypeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}