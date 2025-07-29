using Microsoft.AspNetCore.Mvc;
using ChatDemo.Application;
using ChatDemo.Models;

namespace ChatDemo.Api
{
    /// <summary>
    /// 會員 API 控制器
    /// 處理會員相關的 HTTP 請求
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberApplicationService _memberApplicationService;

        /// <summary>
        /// 建構函數
        /// </summary>
        /// <param name="memberApplicationService">會員應用服務</param>
        public MemberController(MemberApplicationService memberApplicationService)
        {
            _memberApplicationService = memberApplicationService ?? throw new ArgumentNullException(nameof(memberApplicationService));
        }

        /// <summary>
        /// 會員註冊 API
        /// </summary>
        /// <param name="member">會員資料</param>
        /// <returns>註冊結果</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Member member)
        {
            try
            {
                var result = await _memberApplicationService.RegisterMemberAsync(member);
                
                if (result.IsSuccess)
                {
                    return Ok(new { success = true, message = result.Message });
                }
                else
                {
                    return BadRequest(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                // 記錄錯誤日誌（實際應用中應使用 ILogger）
                Console.WriteLine($"會員註冊發生錯誤: {ex.Message}");
                return StatusCode(500, new { success = false, message = "伺服器內部錯誤，請稍後再試" });
            }
        }

        /// <summary>
        /// 會員登入 API
        /// </summary>
        /// <param name="loginRequest">登入請求</param>
        /// <returns>登入結果</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var result = await _memberApplicationService.LoginMemberAsync(loginRequest.Username, loginRequest.Password);
                
                if (result.IsSuccess)
                {
                    return Ok(new { 
                        success = true, 
                        message = result.Message,
                        data = new { 
                            username = result.Data?.Username,
                            email = result.Data?.Email
                        }
                    });
                }
                else
                {
                    return Unauthorized(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                // 記錄錯誤日誌（實際應用中應使用 ILogger）
                Console.WriteLine($"會員登入發生錯誤: {ex.Message}");
                return StatusCode(500, new { success = false, message = "伺服器內部錯誤，請稍後再試" });
            }
        }
    }

    /// <summary>
    /// 登入請求模型
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
} 