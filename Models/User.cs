using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; } = "";

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = "Customer";

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}