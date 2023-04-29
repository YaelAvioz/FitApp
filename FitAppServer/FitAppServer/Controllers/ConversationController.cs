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
    public class ConversationController : ControllerBase
    {
        private static ConversationService _conversationService;
        private static MessageService _messageService;
        private static AccountService _accountService;
        private static MentorService _mentorService;



        public ConversationController(IMapper mapper)
        {
            _conversationService = new ConversationService(mapper);
            _messageService = new MessageService(mapper);
            _accountService = new AccountService(mapper);
            _mentorService = new MentorService(mapper);
        }

        [HttpPost]
        public async Task<ActionResult<ConversationDTO>> Create([FromBody] Conversation entity)
        {
            ConversationDTO newEntity = await _conversationService.Create(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, newEntity);
        }

        [HttpPost("{username}")]
        public async Task<ActionResult<string>> SendMessage(string username, [FromBody] string msg)
        {
            User user = await _accountService.GetUserUsername(username);
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

            // add the mentor's respons to the conversation
            Message mentorMessage = new Message()
            {
                ConversationId = conv.Id,
                Content = answer,
                IsUser = false,
                Timestamp = DateTime.UtcNow
            };
            conv.Messages.Add(mentorMessage.Id);
            return answer;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConversationDTO>> Get(string id)
        {
            ConversationDTO entity = await _conversationService.Get(id);

            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            ConversationDTO entity = await _conversationService.Get(id);

            if (entity == null)
            {
                return NotFound();
            }
            _ = _conversationService.Delete(id);
            return NoContent();
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