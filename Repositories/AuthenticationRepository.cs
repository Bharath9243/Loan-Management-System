using Dapper;
using LoanManagementSystem.Interfaces;
using LoanManagementSystem.Models;
using System.Data;

namespace LoanManagementSystem.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IDbConnection _db;

        public AuthenticationRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<User?> Login(string email, string password)
        {
            string sql = @"
                SELECT *
                FROM Users
                WHERE Email = @Email
                AND PasswordHash = @Password";

            return await _db.QueryFirstOrDefaultAsync<User>(
                sql,
                new
                {
                    Email = email,
                    Password = password
                });
        }

        public async Task<bool> Register(RegisterModel model)
        {
            // Check if email already exists
            string checkSql = @"
                SELECT *
                FROM Users
                WHERE Email = @Email";

            var existingUser = await _db.QueryFirstOrDefaultAsync<User>(
                checkSql,
                new
                {
                    model.Email
                });

            if (existingUser != null)
                return false;

            // Insert new customer
            string insertSql = @"
                INSERT INTO Users
                (
                    Name,
                    Email,
                    PasswordHash,
                    Role,
                    CreatedDate
                )
                VALUES
                (
                    @Name,
                    @Email,
                    @Password,
                    'Customer',
                    @CreatedDate
                )";

            int rows = await _db.ExecuteAsync(
                insertSql,
                new
                {
                    model.Name,
                    model.Email,
                    Password = model.Password,
                    CreatedDate = DateTime.Now
                });

            return rows > 0;
        }
    }
}