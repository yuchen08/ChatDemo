using ChatDemo.Models;

namespace ChatDemo.Contracts
{
    /// <summary>
    /// 會員服務介面契約
    /// 定義會員相關的業務操作
    /// </summary>
    public interface IMemberService
    {
        /// <summary>
        /// 會員註冊
        /// </summary>
        /// <param name="member">會員資料</param>
        /// <returns>註冊是否成功</returns>
        Task<bool> RegisterAsync(Member member);

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="username">用戶名</param>
        /// <param name="password">密碼</param>
        /// <returns>會員資料，如果登入失敗則返回 null</returns>
        Task<Member?> LoginAsync(string username, string password);

        /// <summary>
        /// 檢查用戶名是否已存在
        /// </summary>
        /// <param name="username">用戶名</param>
        /// <returns>是否存在</returns>
        Task<bool> UsernameExistsAsync(string username);
    }
} 