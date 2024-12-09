using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace ResumeAnalyzer.Services
{
    public class AIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AIService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenAI:ApiKey"];
        }

        public async Task<string> AnalyzeResumeAsync(string resumeText, string jobDescription)
        {
            var request = new
            {
                model = "text-davinci-003",
                prompt = $"Analyze the following resume for the job description provided:\n\nResume:\n{resumeText}\n\nJob Description:\n{jobDescription}",
                max_tokens = 1500
            };

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/completions", request);
            // response.EnsureSuccessStatusCode();

            //var result = await response.Content.ReadFromJsonAsync<dynamic>();
            return "Hello, world!";
            //return result.choices[0].text.ToString();
        }
    }
}
