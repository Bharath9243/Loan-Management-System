using Dapper;
using LoanManagementSystem.Interfaces;
using LoanManagementSystem.Models;
using System.Data;

namespace LoanManagementSystem.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IDbConnection _db;

        public DashboardRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<DashboardData> GetDashboardData()
        {
            var dashboard = new DashboardData();

            // Total Customers
            dashboard.UserCount =
                await _db.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM Users");

            // Total Loan Types
            dashboard.CategoryCount =
                await _db.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM LoanTypes");

            // Total Loan Schemes
            dashboard.ProductCount =
                await _db.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM LoanSchemes");

            // Total Loan Applications
            dashboard.OrderCount =
                await _db.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM LoanApplications");

            // Total Loan Amount
            dashboard.Revenue =
                await _db.ExecuteScalarAsync<decimal>(
                    @"
                    SELECT ISNULL(SUM(LoanAmount), 0)
                    FROM LoanApplications");

            // Expected Interest
            dashboard.Profit =
                await _db.ExecuteScalarAsync<decimal>(
                    @"
                    SELECT ISNULL(
                        SUM(LA.LoanAmount * (LS.InterestRate / 100.0)),
                    0)
                    FROM LoanApplications LA
                    INNER JOIN LoanApplicationDetails LAD
                        ON LA.LoanApplicationId = LAD.LoanApplicationId
                    INNER JOIN LoanSchemes LS
                        ON LAD.LoanSchemeId = LS.LoanSchemeId");

            // Total Loan Capacity
            dashboard.InventoryValue =
                await _db.ExecuteScalarAsync<decimal>(
                    @"
                    SELECT ISNULL(
                        SUM(MaximumLoanAmount),
                    0)
                    FROM LoanSchemes");

            // Recent Loan Applications
            dashboard.LatestOrders =
            (
                await _db.QueryAsync<LoanApplication>(
                    @"
                    SELECT TOP 5
                        LA.LoanApplicationId,
                        LA.UserId,
                        LA.ApplicationDate,
                        LA.ApplicationStatus,
                        LA.LoanAmount,
                        U.Name
                    FROM LoanApplications LA
                    INNER JOIN Users U
                        ON LA.UserId = U.UserId
                    ORDER BY LA.ApplicationDate DESC")
            ).ToList();

            // Recently Added Loan Schemes
            dashboard.RecentProducts =
            (
                await _db.QueryAsync<LoanScheme>(
                    @"
                    SELECT TOP 5
                        LoanSchemeId,
                        SchemeName,
                        Description,
                        ProcessingFee,
                        InterestRate,
                        MaximumLoanAmount,
                        LoanTypeId,
                        CreatedDate
                    FROM LoanSchemes
                    ORDER BY CreatedDate DESC")
            ).ToList();

            // Most Applied Loan Schemes
            dashboard.TopSellingProducts =
            (
                await _db.QueryAsync<TopSellingProduct>(
                    @"
                    SELECT TOP 5
                        LS.SchemeName AS ProductName,
                        COUNT(*) AS Quantity
                    FROM LoanApplicationDetails LAD
                    INNER JOIN LoanSchemes LS
                        ON LAD.LoanSchemeId = LS.LoanSchemeId
                    GROUP BY LS.SchemeName
                    ORDER BY Quantity DESC")
            ).ToList();

            return dashboard;
        }
    }
}