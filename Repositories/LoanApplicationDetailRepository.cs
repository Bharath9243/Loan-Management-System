using Dapper;
using LoanManagementSystem.Interfaces;
using LoanManagementSystem.Models;
using System.Data;

namespace LoanManagementSystem.Repositories
{
    public class LoanApplicationDetailRepository : ILoanApplicationDetailRepository
    {
        private readonly IDbConnection _db;

        public LoanApplicationDetailRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<List<LoanApplicationDetail>> GetLoanApplicationDetails(int loanApplicationId)
        {
            string sql = @"
                SELECT
                    LAD.LoanApplicationDetailId,
                    LAD.LoanApplicationId,
                    LAD.LoanSchemeId,
                    LAD.TenureMonths,
                    LAD.InterestRate,
                    LS.SchemeName
                FROM LoanApplicationDetails LAD
                INNER JOIN LoanSchemes LS
                    ON LAD.LoanSchemeId = LS.LoanSchemeId
                WHERE LAD.LoanApplicationId = @LoanApplicationId";

            var items = await _db.QueryAsync<LoanApplicationDetail>(
                sql,
                new { LoanApplicationId = loanApplicationId });

            return items.ToList();
        }

        public async Task CreateLoanApplicationDetail(LoanApplicationDetail detail)
        {
            string sql = @"
                INSERT INTO LoanApplicationDetails
                (
                    LoanApplicationId,
                    LoanSchemeId,
                    TenureMonths,
                    InterestRate
                )
                VALUES
                (
                    @LoanApplicationId,
                    @LoanSchemeId,
                    @TenureMonths,
                    @InterestRate
                )";

            await _db.ExecuteAsync(sql, detail);

            await UpdateLoanApplicationTotal(detail.LoanApplicationId);
        }

        public async Task DeleteLoanApplicationDetail(int loanApplicationDetailId)
        {
            string getApplicationSql = @"
                SELECT LoanApplicationId
                FROM LoanApplicationDetails
                WHERE LoanApplicationDetailId = @Id";

            int loanApplicationId = await _db.QuerySingleAsync<int>(
                getApplicationSql,
                new { Id = loanApplicationDetailId });

            string deleteSql = @"
                DELETE FROM LoanApplicationDetails
                WHERE LoanApplicationDetailId = @Id";

            await _db.ExecuteAsync(deleteSql,
                new { Id = loanApplicationDetailId });

            await UpdateLoanApplicationTotal(loanApplicationId);
        }

        private async Task UpdateLoanApplicationTotal(int loanApplicationId)
        {
            string sql = @"
                UPDATE LoanApplications
                SET LoanAmount =
                (
                    SELECT ISNULL(SUM(TenureMonths * InterestRate), 0)
                    FROM LoanApplicationDetails
                    WHERE LoanApplicationId = @LoanApplicationId
                )
                WHERE LoanApplicationId = @LoanApplicationId";

            await _db.ExecuteAsync(sql,
                new { LoanApplicationId = loanApplicationId });
        }
    }
}