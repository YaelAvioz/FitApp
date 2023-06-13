using FitAppServer.Services;
using FitAppServer.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FitAppServer.DTO;
using FitAppServer.Helper;

namespace FitAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : GenericController<Conversation, ConversationDTO>
    {
        private static ConversationService _conversationService;
        private static FoodService _foodService;
        private static MessageService _messageService;
        private static UserService _userService;
        private static MentorService _mentorService;
        private static string defaultMsg = " and I will be your mentor. Feel free to talk to me every time";

        public ConversationController(IMapper mapper) : base(mapper)
        {
            _conversationService = new ConversationService(mapper);
            _messageService = new MessageService(mapper);
            _userService = new UserService(mapper);
            _mentorService = new MentorService(mapper);
            _foodService = new FoodService(mapper);
        }

        [HttpPost("chat/{username}")]
        public async Task<ActionResult<object>> SendMessage(string username, [FromBody] string msg)
        {
            User user = await _userService.GetUserByUsername(username);
            if (user == null)  return NotFound();            

            Conversation conv = await _conversationService.GetConversation(user.Id);
            if (conv == null) return NotFound();

            // check if there's history before we add the new msg
            List<MessageDTO> lastMsgs = await _messageService.GetConvMsgs(conv.Id);
            string history = "";

            if ((lastMsgs.Count > 0) && (!lastMsgs[lastMsgs.Count - 1].Content.Contains(defaultMsg)))
            {
                history += "\nnotice the context of this conversation:\n";
                foreach (MessageDTO msgDto in lastMsgs)
                {
                    if (msgDto.Content.Contains(defaultMsg)) continue;

                    if (msgDto.IsFromUser)
                    {
                        history += "user: ";
                        history += msgDto.Content;
                    }
                    else
                    {
                        history += "me: ";
                        history += msgDto.Content;
                    }
                    history += "\n";
                }
            }

            // add new message from the client to the conversation
            Message userMessage = new Message()
            {
                ConversationId = conv.Id,
                Content = msg,
                IsFromUser = true,
                Timestamp = DateTime.UtcNow
            };
            conv.Messages.Add(userMessage.Id);
            await _messageService.Create(userMessage);


            // send the message to chatGPT to get an answer
            Mentor mentor = await _mentorService.GetMentorInfo(user.mentor);

            List<Tuple<string, double>> recentFoods = user.foods
                .Where(food => food.Item3.Date == DateTime.Today)
                .Select(food => new Tuple<string, double>(food.Item1, food.Item2))
                .ToList();

            string foodStr = await _foodService.GetFoodForPrompt(recentFoods);

            string prompt = ChatGPT.MessagePrompt(mentor.chat, user.GetChat() + foodStr, msg, history);
            string answer = await ChatGPT.GetAnswer(prompt);

            if (answer == null)
            {
                return BadRequest(new { error = "Sorry, there is a problem. Please try again later" });
            }

            answer = answer.Replace("\n", "");

            // add the mentor's respons to the conversation
            Message mentorMessage = new Message()
            {
                ConversationId = conv.Id,
                Content = answer,
                IsFromUser = false,
                Timestamp = DateTime.UtcNow
            };

            conv.Messages.Add(mentorMessage.Id);
            await _messageService.Create(mentorMessage);
            return new { message = answer.Trim() };
        }


        [HttpGet]
        [Route("{id}/last")]
        public async Task<ActionResult<MessageDTO>> GetLastMessage(string id)
        {
            var conv = await _conversationService.Get(id);

            if (conv == null) return NotFound();
           
            var msgList = conv.Messages;

            if ((msgList == null) || (msgList.Count == 0)) return BadRequest();
            
            return Ok(msgList[-1]);
        }
    }
}