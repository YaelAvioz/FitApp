using System.Net.Http.Headers;
using System.Text;
using FitAppServer.Model;
using Newtonsoft.Json;

namespace FitAppServer.Helper
{
    public class ChatGPT
    {
        public static async Task<string> Chat(Mentor mentor, User user)
        {
            var openAIApiKey = "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";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAIApiKey);

            var requestBody = new
            {
                prompt = GetPrompt(mentor.chat, user.chat),
                temperature = 0.5,
                max_tokens = 50
            };

            var jsonRequest = JsonConvert.SerializeObject(requestBody);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.openai.com/v1/engines/davinci/completions", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                var answer = responseObject.choices[0].text;

                return answer;
            }
            return "There was an error generating a response. Sorry for the inconveniece.";
        }

        private static string GetPrompt(string mc, string uc)
        {
            return mc + "Information about the client – " + uc + ". The message I was given – "
                + " *MSG* ";
        }
    }

}
