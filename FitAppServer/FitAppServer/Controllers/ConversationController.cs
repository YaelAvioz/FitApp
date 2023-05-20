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
        private static MessageService _messageService;
        private static AccountService _accountService;
        private static MentorService _mentorService;

        public ConversationController(IMapper mapper) : base(mapper)
        {
            _conversationService = new ConversationService(mapper);
            _messageService = new MessageService(mapper);
            _accountService = new AccountService(mapper);
            _mentorService = new MentorService(mapper);
        }

        [HttpPost("chat/{username}")]
        public async Task<ActionResult<string>> SendMessage(string username, [FromBody] string msg)
        {
            User user = await _accountService.GetUserByUsername(username);
            if (user == null) 
            {
                return NotFound();
            }

            Conversation conv = await _conversationService.GetConversation(user.Id);
            if (conv == null)
            {
                return NotFound();
            }

            // add new message from the client to the conversation
            Message userMessage = new Message()
            {
                ConversationId = conv.Id,
                Content = msg,
                IsUser = true,
                Timestamp = DateTime.UtcNow
            };
            conv.Messages.Add(userMessage.Id);

            // send the message to chatGPT to get an answer
            Mentor mentor = await _mentorService.GetMentorInfo(user.mentor);
            string answer = await ChatGPT.GetAnswer(user, mentor, msg);

            if (answer == null)
            {
                return "Sorry, There is a problem. Please try again later";
            }

            // add the mentor's respons to the conversation
            Message mentorMessage = new Message()
            {
                ConversationId = conv.Id,
                Content = answer,
                IsUser = false,
                Timestamp = DateTime.UtcNow
            };
            conv.Messages.Add(mentorMessage.Id);
            return answer.Trim();
        }


        [HttpGet]
        [Route("{id}/last")]
        public async Task<ActionResult<MessageDTO>> GetLastMessage(string id)
        {
            var conv = await _conversationService.Get(id);

            if (conv == null)
            {
                return NotFound();
            }

            var msgList = conv.Messages;

            if ((msgList == null) || (msgList.Count == 0))
            {
                return BadRequest();
            }
            
            return Ok(msgList[-1]);
        }
    }
}