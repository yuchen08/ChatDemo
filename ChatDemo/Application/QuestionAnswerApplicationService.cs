using ChatDemo.Contracts;

namespace ChatDemo.Application
{
    /// <summary>
    /// 問答應用服務
    /// 處理 AI 問答相關的業務邏輯和驗證
    /// </summary>
    public class QuestionAnswerApplicationService
    {
        private readonly IQuestionAnswerService _questionAnswerService;

        public QuestionAnswerApplicationService(IQuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }

        /// <summary>
        /// 處理問題回答業務邏輯
        /// </summary>
        /// <param name="question">問題內容</param>
        /// <returns>回答結果</returns>
        public async Task<ApplicationResult<string>> GetAnswerAsync(string question)
        {
            // 業務驗證
            if (string.IsNullOrWhiteSpace(question))
            {
                return ApplicationResult<string>.Failure("問題內容不能為空");
            }

            if (question.Length > 1000)
            {
                return ApplicationResult<string>.Failure("問題內容過長，請縮短問題");
            }

            // 過濾不當內容（簡單示例）
            if (ContainsInappropriateContent(question))
            {
                return ApplicationResult<string>.Failure("問題內容包含不當詞彙，請重新輸入");
            }

            try
            {
                // 執行問答
                var answer = await _questionAnswerService.GetAnswerAsync(question);
                
                if (!string.IsNullOrWhiteSpace(answer))
                {
                    return ApplicationResult<string>.Success(answer, "回答成功");
                }
                else
                {
                    return ApplicationResult<string>.Failure("無法生成回答，請稍後再試");
                }
            }
            catch (Exception ex)
            {
                // 記錄錯誤日誌（實際應用中應使用 ILogger）
                Console.WriteLine($"問答服務發生錯誤: {ex.Message}");
                return ApplicationResult<string>.Failure("服務暫時不可用，請稍後再試");
            }
        }

        /// <summary>
        /// 檢查是否包含不當內容（簡單示例）
        /// </summary>
        /// <param name="content">檢查內容</param>
        /// <returns>是否包含不當內容</returns>
        private bool ContainsInappropriateContent(string content)
        {
            // 這裡只是簡單示例，實際應用中應該有更完善的內容過濾機制
            var inappropriateWords = new[] { "暴力", "色情", "政治敏感" };
            return inappropriateWords.Any(word => content.Contains(word));
        }
    }
} 