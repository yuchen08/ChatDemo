using ChatDemo.Contracts;
using ChatDemo.Models;

namespace ChatDemo.Application
{
    /// <summary>
    /// 會員應用服務
    /// 處理會員相關的業務邏輯和驗證
    /// </summary>
    public class MemberApplicationService
    {
        private readonly IMemberService _memberService;

        public MemberApplicationService(IMemberService memberService)
        {
            _memberService = memberService;
        }

        /// <summary>
        /// 處理會員註冊業務邏輯
        /// </summary>
        /// <param name="member">會員資料</param>
        /// <returns>註冊結果</returns>
        public async Task<ApplicationResult<bool>> RegisterMemberAsync(Member member)
        {
            // 業務驗證
            if (string.IsNullOrWhiteSpace(member.Username) || string.IsNullOrWhiteSpace(member.Password))
            {
                return ApplicationResult<bool>.Failure("用戶名和密碼不能為空");
            }

            if (member.Username.Length < 3)
            {
                return ApplicationResult<bool>.Failure("用戶名至少需要3個字符");
            }

            if (member.Password.Length < 6)
            {
                return ApplicationResult<bool>.Failure("密碼至少需要6個字符");
            }

            // 檢查用戶名是否已存在
            if (await _memberService.UsernameExistsAsync(member.Username))
            {
                return ApplicationResult<bool>.Failure("用戶名已存在");
            }

            // 執行註冊
            var result = await _memberService.RegisterAsync(member);
            
            if (result)
            {
                return ApplicationResult<bool>.Success(true, "註冊成功");
            }
            else
            {
                return ApplicationResult<bool>.Failure("註冊失敗，請稍後再試");
            }
        }

        /// <summary>
        /// 處理會員登入業務邏輯
        /// </summary>
        /// <param name="username">用戶名</param>
        /// <param name="password">密碼</param>
        /// <returns>登入結果</returns>
        public async Task<ApplicationResult<Member>> LoginMemberAsync(string username, string password)
        {
            // 業務驗證
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return ApplicationResult<Member>.Failure("用戶名和密碼不能為空");
            }

            // 執行登入
            var member = await _memberService.LoginAsync(username, password);
            
            if (member != null)
            {
                return ApplicationResult<Member>.Success(member, "登入成功");
            }
            else
            {
                return ApplicationResult<Member>.Failure("用戶名或密碼不正確");
            }
        }
    }

    /// <summary>
    /// 應用層結果封裝類
    /// </summary>
    /// <typeparam name="T">結果資料類型</typeparam>
    public class ApplicationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public T? Data { get; private set; }

        private ApplicationResult(bool isSuccess, string message, T? data = default)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static ApplicationResult<T> Success(T data, string message = "操作成功")
        {
            return new ApplicationResult<T>(true, message, data);
        }

        public static ApplicationResult<T> Failure(string message)
        {
            return new ApplicationResult<T>(false, message);
        }
    }
} 