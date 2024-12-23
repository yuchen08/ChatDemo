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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Member member)
        {
            if (await _memberService.UsernameExistsAsync(member.Username))
            {
                return BadRequest("Username already exists.");
            }

            // Hash 密碼
            member.Password = BCrypt.Net.BCrypt.HashPassword(member.Password);
            var result = await _memberService.RegisterAsync(member);

            if (!result)
            {
                return StatusCode(500, "Failed to register member.");
            }

            return Ok("Member registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string username, [FromQuery] string password)
        {
            var member = await _memberService.LoginAsync(username, password);
            if (member == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok("Login successful.");
        }
    }
}
