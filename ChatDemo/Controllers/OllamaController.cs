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
                // �T�O�o�̨S���ɭP�L�a�ШD���޿�
                QuestionAnswerService OllamaService = new QuestionAnswerService();
                var answer = await OllamaService.GetAnswerAsync(question);
                return Ok(answer);
            }
            catch (Exception ex)
            {
                // ��^���~�T��
                return BadRequest($"�X���F: {ex.Message}");
            }
        }
    }
}