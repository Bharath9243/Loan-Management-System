using LoanManagementSystem.Models;

namespace LoanManagementSystem.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task<User?> GetUserById(int id);

        Task CreateUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(int id);
    }
}