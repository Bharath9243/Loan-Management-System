using LoanManagementSystem.Models;

namespace LoanManagementSystem.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<User?> Login(string email, string password);

        Task<bool> Register(RegisterModel model);
    }
}