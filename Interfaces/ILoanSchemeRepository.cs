using LoanManagementSystem.Models;

namespace LoanManagementSystem.Interfaces
{
    public interface ILoanSchemeRepository
    {
        Task<List<LoanScheme>> GetAllLoanSchemes();

        Task<LoanScheme?> GetLoanSchemeById(int id);

        Task CreateLoanScheme(LoanScheme loanScheme);

        Task UpdateLoanScheme(LoanScheme loanScheme);

        Task DeleteLoanScheme(int id);
    }
}