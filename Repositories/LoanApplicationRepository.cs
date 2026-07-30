using Dapper;
using LoanManagementSystem.Interfaces;
using LoanManagementSystem.Models;
using System.Data;

namespace LoanManagementSystem.Repositories
{
    public class LoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly IDbConnection _db;

        public LoanApplicationRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<List<LoanApplication>> GetAllLoanApplications()
        {
            string sql = @"
                SELECT
                    LA.LoanApplicationId,
                    LA.UserId,
                    LA.ApplicationDate,
                    LA.ApplicationStatus,
                    LA.LoanAmount,
                    U.Name
                FROM LoanApplications LA
                INNER JOIN Users U
                    ON LA.UserId = U.UserId";

            var applications = await _db.QueryAsync<LoanApplication>(sql);

            return applications.ToList();
        }

        public async Task<List<LoanApplication>> GetLoanApplicationsByUserId(int userId)
        {
            string sql = @"
                SELECT
                    LA.LoanApplicationId,
                    LA.UserId,
                    LA.ApplicationDate,
                    LA.ApplicationStatus,
                    LA.LoanAmount,
                    U.Name
                FROM LoanApplications LA
                INNER JOIN Users U
                    ON LA.UserId = U.UserId
                WHERE LA.UserId = @UserId";

            var applications = await _db.QueryAsync<LoanApplication>(
                sql,
                new { UserId = userId });

            return applications.ToList();
        }

        public async Task<List<User>> GetAllUsers()
        {
            string sql = "SELECT * FROM Users";

            var users = await _db.QueryAsync<User>(sql);

            return users.ToList();
        }

        public async Task<LoanApplication?> GetLoanApplicationById(int id)
        {
            string sql = @"
                SELECT
                    LA.LoanApplicationId,
                    LA.UserId,
                    LA.ApplicationDate,
                    LA.ApplicationStatus,
                    LA.LoanAmount,
                    U.Name
                FROM LoanApplications LA
                INNER JOIN Users U
                    ON LA.UserId = U.UserId
                WHERE LA.LoanApplicationId = @Id";

            return await _db.QueryFirstOrDefaultAsync<LoanApplication>(
                sql,
                new { Id = id });
        }

        public async Task CreateLoanApplication(LoanApplication application)
        {
            string sql = @"
                INSERT INTO LoanApplications
                (
                    UserId,
                    ApplicationDate,
                    ApplicationStatus,
                    LoanAmount
                )
                VALUES
                (
                    @UserId,
                    @ApplicationDate,
                    @ApplicationStatus,
                    @LoanAmount
                )";

            await _db.ExecuteAsync(sql, application);
        }

        public async Task UpdateLoanApplication(LoanApplication application)
        {
            string sql = @"
                UPDATE LoanApplications
                SET
                    UserId = @UserId,
                    ApplicationDate = @ApplicationDate,
                    ApplicationStatus = @ApplicationStatus,
                    LoanAmount = @LoanAmount
                WHERE LoanApplicationId = @LoanApplicationId";

            await _db.ExecuteAsync(sql, application);
        }

        public async Task DeleteLoanApplication(int id)
        {
            string sql = @"
                DELETE FROM LoanApplications
                WHERE LoanApplicationId = @Id";

            await _db.ExecuteAsync(sql, new { Id = id });
        }

        public async Task UpdateApplicationStatus(int loanApplicationId, string status)
        {
            string sql = @"
        UPDATE LoanApplications
        SET ApplicationStatus = @Status
        WHERE LoanApplicationId = @LoanApplicationId";

            await _db.ExecuteAsync(sql,
                new
                {
                    LoanApplicationId = loanApplicationId,
                    Status = status
                });
        }
    }
}