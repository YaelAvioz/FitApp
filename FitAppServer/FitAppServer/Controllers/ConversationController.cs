using FitAppServer.Services;
using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using FitAppServer.Interfaces;

namespace FitAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : GenericController<Conversation, ConversationDTO>
    {
        private static ConversationService _conversationService;
        private static MessageService _messageService;


        public ConversationController(IMapper mapper) : base(mapper)
        {
            _conversationService = new ConversationService(mapper);
            _messageService = new MessageService(mapper);
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