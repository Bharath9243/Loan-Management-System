using LoanManagementSystem.Models;

namespace LoanManagementSystem.Interfaces
{
    public interface ILoanApplicationRepository
    {
        Task<List<LoanApplication>> GetAllLoanApplications();

        Task<List<LoanApplication>> GetLoanApplicationsByUserId(int userId);

        Task<List<User>> GetAllUsers();

        Task<LoanApplication?> GetLoanApplicationById(int id);

        Task CreateLoanApplication(LoanApplication order);

        Task UpdateLoanApplication(LoanApplication order);

        Task DeleteLoanApplication(int id);
    }
}