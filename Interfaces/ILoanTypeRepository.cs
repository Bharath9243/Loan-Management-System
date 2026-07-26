using LoanManagementSystem.Models;

namespace LoanManagementSystem.Interfaces
{
    public interface ILoanTypeRepository
    {
        Task<List<LoanType>> GetAllLoanTypes();

        Task<LoanType?> GetLoanTypeById(int id);

        Task CreateLoanType(LoanType category);

        Task UpdateLoanType(LoanType category);

        Task DeleteLoanType(int id);
    }
}