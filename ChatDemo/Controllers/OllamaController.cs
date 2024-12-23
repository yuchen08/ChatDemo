using Microsoft.AspNetCore.Mvc;
using ChatDemo.Services;

namespace ChatDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OllamaController : ControllerBase
    {
        private readonly QuestionAnswerService _ollamaService;

        public OllamaController(QuestionAnswerService ollamaService)
        {
            _ollamaService = ollamaService;
        }

        [HttpGet("question/{question}")]
        public async Task<ActionResult<string>> GetAnswer(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                return BadRequest("問題內容不能為空");
            }

            try
            {
                var answer = await _ollamaService.GetAnswerAsync(question);
                return Ok(answer);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes (e.g., using ILogger)
                return StatusCode(500, "伺服器內部錯誤，請稍後再試。");
            }
        }
    }
}
