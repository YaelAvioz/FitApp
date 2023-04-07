using FitAppServer.Services;
using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

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

    }
}