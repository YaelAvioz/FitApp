using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;

namespace FitAppServer.Services
{
    public class ConversationService : GenericService<Conversation, ConversationDTO>
    {
        public ConversationService(IMapper mapper) : base(mapper) { }

        public async Task<Conversation> GetConversation(string userId)
        {
            var conv = await _collection.Find(x => x.UserId.Equals(userId)).FirstOrDefaultAsync();
            return conv;
        }

    }
}