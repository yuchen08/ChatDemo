using ChatDemo.Models;
using Dapper;
using static ChatDemo.Services.DapperDatabaseService;

namespace ChatDemo.Services
{
    public class DapperMemberService
    {
        private readonly IDatabaseService _databaseService;

        public DapperMemberService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> RegisterAsync(Member member)
        {
            const string query = @"
                INSERT INTO Members (Username, Password, Email) 
                VALUES (@Username, @Password, @Email)";

            using var connection = _databaseService.CreateConnection();
            var result = await connection.ExecuteAsync(query, member);
            return result > 0;
        }

        public async Task<Member?> LoginAsync(string username, string password)
        {
            const string query = @"
                SELECT * FROM Members WHERE Username = @Username";

            using var connection = _databaseService.CreateConnection();
            var member = await connection.QuerySingleOrDefaultAsync<Member>(query, new { Username = username });

            if (member != null && BCrypt.Net.BCrypt.Verify(password, member.Password))
            {
                return member;
            }

            return null;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            const string query = @"
                SELECT COUNT(1) FROM Members WHERE Username = @Username";

            using var connection = _databaseService.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(query, new { Username = username });
            return count > 0;
        }
    }
}