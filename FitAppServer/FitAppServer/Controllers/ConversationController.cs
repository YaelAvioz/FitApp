using FitAppServer.Services;
using FitAppServer.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FitAppServer.DTO;

namespace FitAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private static ConversationService _conversationService;
        private static MessageService _messageService;


        public ConversationController(IMapper mapper)
        {
            _conversationService = new ConversationService(mapper);
            _messageService = new MessageService(mapper);
        }

        [HttpPost]
        public async Task<ActionResult<ConversationDTO>> Create([FromBody] Conversation entity)
        {
            ConversationDTO newEntity = await _conversationService.Create(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, newEntity);
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