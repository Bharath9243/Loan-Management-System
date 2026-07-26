using Dapper;
using LoanManagementSystem.Interfaces;
using LoanManagementSystem.Models;
using System.Data;

namespace LoanManagementSystem.Repositories
{
    public class LoanTypeRepository : ILoanTypeRepository
    {
        private readonly IDbConnection _db;

        public LoanTypeRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<List<LoanType>> GetAllLoanTypes()
        {
            string sql = @"
                SELECT *
                FROM LoanTypes";

            var loanTypes = await _db.QueryAsync<LoanType>(sql);

            return loanTypes.ToList();
        }

        public async Task<LoanType?> GetLoanTypeById(int id)
        {
            string sql = @"
                SELECT *
                FROM LoanTypes
                WHERE LoanTypeId = @Id";

            return await _db.QueryFirstOrDefaultAsync<LoanType>(sql, new { Id = id });
        }

        public async Task CreateLoanType(LoanType loanType)
        {
            string sql = @"
                INSERT INTO LoanTypes
                (
                    LoanTypeName,
                    Description,
                    CreatedDate
                )
                VALUES
                (
                    @LoanTypeName,
                    @Description,
                    @CreatedDate
                );";

            await _db.ExecuteAsync(sql, loanType);
        }

        public async Task UpdateLoanType(LoanType loanType)
        {
            string sql = @"
                UPDATE LoanTypes
                SET
                    LoanTypeName = @LoanTypeName,
                    Description = @Description
                WHERE LoanTypeId = @LoanTypeId;";

            await _db.ExecuteAsync(sql, loanType);
        }

        public async Task DeleteLoanType(int id)
        {
            string sql = @"
                DELETE FROM LoanTypes
                WHERE LoanTypeId = @Id;";

            await _db.ExecuteAsync(sql, new { Id = id });
        }
    }
}