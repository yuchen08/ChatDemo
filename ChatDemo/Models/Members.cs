namespace ChatDemo.Models
{
    /// <summary>
    /// 會員資料模型
    /// 定義會員的基本資料結構
    /// </summary>
    public class Member
    {
        /// <summary>
        /// 會員唯一識別碼
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 會員用戶名
        /// 用於登入和顯示
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 會員密碼
        /// 儲存時會進行雜湊加密
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 會員電子郵件
        /// 用於聯絡和驗證
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}