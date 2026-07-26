using LoanManagementSystem.Models;

namespace LoanManagementSystem.Interfaces
{
    public interface ILoanApplicationDetailRepository
    {
        Task<List<LoanApplicationDetail>> GetLoanApplicationDetails(int orderId);

        Task CreateLoanApplicationDetail(LoanApplicationDetail item);

        Task DeleteLoanApplicationDetail(int id);
    }
}