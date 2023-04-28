using System.Net.Http.Headers;
using System.Text;
using OpenAI_API;
using FitAppServer.Model;
using Newtonsoft.Json;
using OpenAI_API.Completions;

namespace FitAppServer.Helper
{
    public static class ChatGPT
    {
        public static async Task<string> GetAnswer(User user, Mentor mentor, string userMsg)
        {
            var openAIApiKey = ApiKey.key;
            var openAI = new OpenAIAPI(openAIApiKey);

            var prompt = GetPrompt(mentor.chat, user.getChat(), userMsg);
            var completionRequest = new CompletionRequest()
            {
                Prompt = prompt,
                Temperature = 0.5,
                MaxTokens = 50
            };

            var response = await openAI.Completions.CreateCompletionAsync(completionRequest);

            if (response != null)
            {
                var answer = response.Completions[0].Text;
                return answer;
            }
            return "There was an error generating a response. Sorry for the inconveniece.";
        }

        private static string GetPrompt(string mc, string uc, string msg)
        {
            return mc + "Information about the client – " + uc + ". The message I was given – "
                + " " + msg;
        }
    }
}
