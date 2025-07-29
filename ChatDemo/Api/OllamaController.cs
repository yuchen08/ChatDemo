using Microsoft.AspNetCore.Mvc;
using ChatDemo.Application;

namespace ChatDemo.Api
{
    /// <summary>
    /// Ollama AI 問答 API 控制器
    /// 處理 AI 問答相關的 HTTP 請求
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OllamaController : ControllerBase
    {
        private readonly QuestionAnswerApplicationService _questionAnswerApplicationService;

        /// <summary>
        /// 建構函數
        /// </summary>
        /// <param name="questionAnswerApplicationService">問答應用服務</param>
        public OllamaController(QuestionAnswerApplicationService questionAnswerApplicationService)
        {
            _questionAnswerApplicationService = questionAnswerApplicationService ?? throw new ArgumentNullException(nameof(questionAnswerApplicationService));
        }

        /// <summary>
        /// 取得 AI 回答 API
        /// </summary>
        /// <param name="question">問題內容</param>
        /// <returns>AI 回答</returns>
        [HttpGet("question/{question}")]
        public async Task<ActionResult<string>> GetAnswer(string question)
        {
            try
            {
                var result = await _questionAnswerApplicationService.GetAnswerAsync(question);
                
                if (result.IsSuccess)
                {
                    return Ok(new { 
                        success = true, 
                        message = result.Message,
                        data = result.Data 
                    });
                }
                else
                {
                    return BadRequest(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                // 記錄錯誤日誌（實際應用中應使用 ILogger）
                Console.WriteLine($"AI 問答發生錯誤: {ex.Message}");
                return StatusCode(500, new { success = false, message = "伺服器內部錯誤，請稍後再試" });
            }
        }

        /// <summary>
        /// 取得 AI 回答 API (POST 方法)
        /// </summary>
        /// <param name="request">問答請求</param>
        /// <returns>AI 回答</returns>
        [HttpPost("question")]
        public async Task<IActionResult> GetAnswerPost([FromBody] QuestionRequest request)
        {
            try
            {
                var result = await _questionAnswerApplicationService.GetAnswerAsync(request.Question);
                
                if (result.IsSuccess)
                {
                    return Ok(new { 
                        success = true, 
                        message = result.Message,
                        data = result.Data 
                    });
                }
                else
                {
                    return BadRequest(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                // 記錄錯誤日誌（實際應用中應使用 ILogger）
                Console.WriteLine($"AI 問答發生錯誤: {ex.Message}");
                return StatusCode(500, new { success = false, message = "伺服器內部錯誤，請稍後再試" });
            }
        }
    }

    /// <summary>
    /// 問答請求模型
    /// </summary>
    public class QuestionRequest
    {
        public string Question { get; set; } = string.Empty;
    }
} 