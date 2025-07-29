using OllamaSharp;
using Microsoft.Extensions.Configuration;
using ChatDemo.Contracts;

namespace ChatDemo.Implementation
{
    /// <summary>
    /// Ollama AI 問答服務實作
    /// 使用 Ollama 進行 AI 問答功能
    /// </summary>
    public class OllamaService : IQuestionAnswerService
    {
        private readonly string _ollamaBaseUrl;
        private readonly string _model;

        /// <summary>
        /// 建構函數
        /// </summary>
        /// <param name="configuration">配置服務</param>
        public OllamaService(IConfiguration configuration)
        {
            var ollamaSettings = configuration.GetSection("OllamaSettings");
            _ollamaBaseUrl = ollamaSettings["BaseUrl"] 
                ?? throw new ArgumentNullException(nameof(configuration), "Ollama BaseUrl 未配置");
            _model = ollamaSettings["Model"] 
                ?? throw new ArgumentNullException(nameof(configuration), "Ollama Model 未配置");
        }

        /// <summary>
        /// 取得 AI 回答
        /// </summary>
        /// <param name="prompt">問題提示</param>
        /// <returns>AI 回答內容</returns>
        public async Task<string> GetAnswerAsync(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                return "輸入提示不能為空。";
            }

            var ollama = new OllamaApiClient(_ollamaBaseUrl)
            {
                SelectedModel = _model
            };
            var chat = new Chat(ollama);

            try
            {
                var responseStream = chat.SendAsync(prompt);
                string fullResponse = "";

                await foreach (var response in responseStream)
                {
                    if (!string.IsNullOrEmpty(response))
                    {
                        fullResponse += response.Trim();
                    }
                }

                return fullResponse;
            }
            catch (Exception ex)
            {
                // 記錄異常 (實際應用中應使用 ILogger)
                Console.WriteLine($"Ollama 服務發生錯誤: {ex.Message}");
                return "伺服器內部錯誤，請稍後再試。";
            }
        }
    }
} 