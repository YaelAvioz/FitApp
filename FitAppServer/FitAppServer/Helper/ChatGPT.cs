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
        
        
        public static string GradePrompt(User user)
        {

            return "If I'm a " + user.gender + ", I'm " + user.age.ToString() + ", I weight " + user.weight[user.weight.Count - 1].Item1.ToString() +
                " kg and my height is " + user.height.ToString() + " meters. \n" +
                "How many calories should I eat per day ? how much total_fat, calcium, protein, carbohydrate, fiber, sugars, and fat ? \n\n" +
                "DO NOT explain. write me the answer based on your reasarch as a string in this format: \r\n" +
                "1200, 40, 1000, 50, 180, 25, 30, 50";
        }


    }
}
