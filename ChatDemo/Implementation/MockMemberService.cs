using ChatDemo.Contracts;
using ChatDemo.Models;

namespace ChatDemo.Implementation
{
    /// <summary>
    /// 會員服務的 Mock 實作，僅用於測試或開發環境
    /// </summary>
    public class MockMemberService : IMemberService
    {
        private static readonly List<Member> _mockMembers = new List<Member>
        {
            new Member { Id = 1, Username = "testuser", Password = BCrypt.Net.BCrypt.HashPassword("123456"), Email = "testuser@example.com" },
            new Member { Id = 2, Username = "alice", Password = BCrypt.Net.BCrypt.HashPassword("alicepwd"), Email = "alice@example.com" }
        };

        public Task<bool> RegisterAsync(Member member)
        {
            if (_mockMembers.Any(m => m.Username == member.Username))
                return Task.FromResult(false);
            member.Id = _mockMembers.Max(m => m.Id) + 1;
            member.Password = BCrypt.Net.BCrypt.HashPassword(member.Password);
            _mockMembers.Add(member);
            return Task.FromResult(true);
        }

        public Task<Member?> LoginAsync(string username, string password)
        {
            var member = _mockMembers.FirstOrDefault(m => m.Username == username);
            if (member != null && BCrypt.Net.BCrypt.Verify(password, member.Password))
                return Task.FromResult<Member?>(member);
            return Task.FromResult<Member?>(null);
        }

        public Task<bool> UsernameExistsAsync(string username)
        {
            return Task.FromResult(_mockMembers.Any(m => m.Username == username));
        }
    }
} 