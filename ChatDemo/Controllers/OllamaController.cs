using Microsoft.AspNetCore.Mvc;
using ChatDemo.Service;

namespace ChatDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OllamaController : ControllerBase
    {
        [HttpGet("{question}")]
        public async Task<ActionResult<string>> GetById(string question)
        {
            try
            {
                // 確保這裡沒有導致無窮請求的邏輯
                QuestionAnswerService OllamaService = new QuestionAnswerService();
                var answer = await OllamaService.GetAnswerAsync(question);
                return Ok(answer);
            }
            catch (Exception ex)
            {
                // 返回錯誤訊息
                return BadRequest($"出錯了: {ex.Message}");
            }
        }
    }
}