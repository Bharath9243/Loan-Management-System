using Dapper;
using LoanManagementSystem.Interfaces;
using LoanManagementSystem.Models;
using System.Data;

namespace LoanManagementSystem.Repositories
{
    public class LoanSchemeRepository : ILoanSchemeRepository
    {
        private readonly IDbConnection _db;

        public LoanSchemeRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<List<LoanScheme>> GetAllLoanSchemes()
        {
            string sql = @"
                SELECT
                    LS.LoanSchemeId,
                    LS.SchemeName,
                    LS.Description,
                    LS.ProcessingFee,
                    LS.InterestRate,
                    LS.MaximumLoanAmount,
                    LS.LoanTypeId,
                    LT.LoanTypeName,
                    LS.CreatedDate
                FROM LoanSchemes LS
                INNER JOIN LoanTypes LT
                    ON LS.LoanTypeId = LT.LoanTypeId
                ORDER BY LS.SchemeName;";

            var loanSchemes = await _db.QueryAsync<LoanScheme>(sql);

            return loanSchemes.ToList();
        }

        public async Task<LoanScheme?> GetLoanSchemeById(int id)
        {
            string sql = @"
                SELECT
                    LS.LoanSchemeId,
                    LS.SchemeName,
                    LS.Description,
                    LS.ProcessingFee,
                    LS.InterestRate,
                    LS.MaximumLoanAmount,
                    LS.LoanTypeId,
                    LT.LoanTypeName,
                    LS.CreatedDate
                FROM LoanSchemes LS
                INNER JOIN LoanTypes LT
                    ON LS.LoanTypeId = LT.LoanTypeId
                WHERE LS.LoanSchemeId = @Id;";

            return await _db.QueryFirstOrDefaultAsync<LoanScheme>(sql, new { Id = id });
        }

        public async Task CreateLoanScheme(LoanScheme loanScheme)
        {
            string sql = @"
                INSERT INTO LoanSchemes
                (
                    SchemeName,
                    Description,
                    ProcessingFee,
                    InterestRate,
                    MaximumLoanAmount,
                    LoanTypeId,
                    CreatedDate
                )
                VALUES
                (
                    @SchemeName,
                    @Description,
                    @ProcessingFee,
                    @InterestRate,
                    @MaximumLoanAmount,
                    @LoanTypeId,
                    @CreatedDate
                );";

            await _db.ExecuteAsync(sql, loanScheme);
        }

        public async Task UpdateLoanScheme(LoanScheme loanScheme)
        {
            string sql = @"
                UPDATE LoanSchemes
                SET
                    SchemeName = @SchemeName,
                    Description = @Description,
                    ProcessingFee = @ProcessingFee,
                    InterestRate = @InterestRate,
                    MaximumLoanAmount = @MaximumLoanAmount,
                    LoanTypeId = @LoanTypeId
                WHERE LoanSchemeId = @LoanSchemeId;";

            await _db.ExecuteAsync(sql, loanScheme);
        }

        public async Task DeleteLoanScheme(int id)
        {
            string sql = @"
                DELETE FROM LoanSchemes
                WHERE LoanSchemeId = @Id;";

            await _db.ExecuteAsync(sql, new { Id = id });
        }
    }
}