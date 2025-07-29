namespace ChatDemo.Contracts
{
    /// <summary>
    /// 問答服務介面契約
    /// 定義 AI 問答相關的操作
    /// </summary>
    public interface IQuestionAnswerService
    {
        /// <summary>
        /// 取得 AI 回答
        /// </summary>
        /// <param name="prompt">問題提示</param>
        /// <returns>AI 回答內容</returns>
        Task<string> GetAnswerAsync(string prompt);
    }
} 