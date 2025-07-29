using ChatDemo.Contracts;

namespace ChatDemo.Implementation
{
    /// <summary>
    /// 問答服務的 Mock 實作，僅用於測試或開發環境
    /// </summary>
    public class MockQuestionAnswerService : IQuestionAnswerService
    {
        public Task<string> GetAnswerAsync(string prompt)
        {
            // 回傳固定的假資料
            return Task.FromResult($"[Mock 回覆] 你問了：{prompt}，這是測試用的 AI 回答。");
        }
    }
} 