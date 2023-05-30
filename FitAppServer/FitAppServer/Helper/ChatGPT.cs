using OpenAI_API;
using FitAppServer.Model;
using OpenAI_API.Completions;

namespace FitAppServer.Helper
{
    public static class ChatGPT
    {
        public static async Task<string> GetAnswer(string prompt)
        {
            var openAIApiKey = ApiKey.key;
            var openAI = new OpenAIAPI(openAIApiKey);

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
                return answer.ToString();
            }
            return "There was an error generating a response. Sorry for the inconveniece.";
        }

        public static string MessagePrompt(string mc, string uc, string msg, string history)
        {
            return mc + "Info about the client: " + uc + history + ". The message I was sent now: " + msg + "\n" +"what should I answer?";
        }

        private static string InsightsPrompt(string mc, string uc, string msg, string history)
        {
            return mc + "Info about the client: " + uc + history + ". The message I was sent now: " + msg + "\n";
        }


    }
}
