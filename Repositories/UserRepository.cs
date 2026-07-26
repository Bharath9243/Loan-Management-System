using Dapper;
using LoanManagementSystem.Models;
using System.Data;
using System.Data.Common;
using LoanManagementSystem.Interfaces;

namespace LoanManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAllUsers()
        {
            string sql = "SELECT * FROM Users";

            var users = await _db.QueryAsync<User>(sql);

            return users.ToList();
        }

        public async Task CreateUser(User user)
        {
            var sql = @"
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
            @PasswordHash,
            @Role,
            @CreatedDate
        );";

            await _db.ExecuteAsync(sql, user);
        }
        public async Task<User?> GetUserById(int id)
        {
            string sql = "SELECT * FROM Users WHERE UserId = @Id";

            return await _db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task UpdateUser(User user)
        {
            string sql = @"
        UPDATE Users
        SET
            Name = @Name,
            Email = @Email,
            PasswordHash = @PasswordHash,
            Role = @Role
        WHERE UserId = @UserId";

            await _db.ExecuteAsync(sql, user);
        }

        public async Task DeleteUser(int id)
        {
            string sql = "DELETE FROM Users WHERE UserId = @Id";

            await _db.ExecuteAsync(sql, new { Id = id });
        }
    }
}
