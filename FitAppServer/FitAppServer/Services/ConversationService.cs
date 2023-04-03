using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;

namespace FitAppServer.Services
{
    public class ConversationService : GenericService<Conversation, ConversationDTO>
    {
        public ConversationService(IMapper mapper) : base(mapper) { }

    }
}