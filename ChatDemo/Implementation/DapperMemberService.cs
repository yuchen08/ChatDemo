using ChatDemo.Contracts;
using ChatDemo.Models;
using Dapper;

namespace ChatDemo.Implementation
{
    /// <summary>
    /// Dapper 會員服務實作
    /// 使用 Dapper 進行會員資料的資料庫操作
    /// </summary>
    public class DapperMemberService : IMemberService
    {
        private readonly IDatabaseService _databaseService;

        /// <summary>
        /// 建構函數
        /// </summary>
        /// <param name="databaseService">資料庫服務</param>
        public DapperMemberService(IDatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }

        /// <summary>
        /// 會員註冊
        /// </summary>
        /// <param name="member">會員資料</param>
        /// <returns>註冊是否成功</returns>
        public async Task<bool> RegisterAsync(Member member)
        {
            // 1. 註冊前先雜湊密碼
            member.Password = BCrypt.Net.BCrypt.HashPassword(member.Password);

            const string query = @"
                INSERT INTO Members (Username, Password, Email) 
                VALUES (@Username, @Password, @Email)";

            using var connection = _databaseService.CreateConnection();
            var result = await connection.ExecuteAsync(query, member);
            return result > 0;
        }

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="username">用戶名</param>
        /// <param name="password">密碼</param>
        /// <returns>會員資料，如果登入失敗則返回 null</returns>
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

        /// <summary>
        /// 檢查用戶名是否已存在
        /// </summary>
        /// <param name="username">用戶名</param>
        /// <returns>是否存在</returns>
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