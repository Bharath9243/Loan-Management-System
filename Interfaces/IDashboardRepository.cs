using LoanManagementSystem.Models;

namespace LoanManagementSystem.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardData> GetDashboardData();
    }
}