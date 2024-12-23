using OllamaSharp;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ChatDemo.Services
{
    public class QuestionAnswerService
    {
        private readonly string _ollamaBaseUrl;
        private readonly string _model;

        public QuestionAnswerService(IConfiguration configuration)
        {
            var ollamaSettings = configuration.GetSection("OllamaSettings");
            _ollamaBaseUrl = ollamaSettings["BaseUrl"];
            _model = ollamaSettings["Model"];
        }

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
                // 記錄異常 (可替換為 ILogger)
                Console.WriteLine($"發生錯誤: {ex}");
                return "伺服器內部錯誤，請稍後再試。";
            }
        }
    }
}
