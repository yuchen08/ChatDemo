using ChatDemo.Models;
using ChatDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperMemberController : ControllerBase
    {
        private readonly DapperMemberService _memberService;

        public DapperMemberController(DapperMemberService memberService)
        {
            _memberService = memberService;
        }

        // 註冊功能
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Member member)
        {
            // 檢查用戶名是否已存在
            if (await _memberService.UsernameExistsAsync(member.Username))
            {
                return BadRequest("用戶名已存在。");
            }

            // 雜湊密碼
            member.Password = BCrypt.Net.BCrypt.HashPassword(member.Password);
            var result = await _memberService.RegisterAsync(member);

            if (!result)
            {
                return StatusCode(500, "註冊失敗，請稍後再試。");
            }

            return Ok("會員註冊成功。");
        }

        // 登入功能
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Member member)
        {
            var members = await _memberService.LoginAsync(member.Username, member.Password);
            if (members == null)
            {
                return Unauthorized(new { success = false, message = "用戶名或密碼不正確。" });
            }

            return Ok(new { success = true, message = "登入成功。" });
        }
    }
}
