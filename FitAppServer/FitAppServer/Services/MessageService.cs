using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;

namespace FitAppServer.Services
{
    public class MessageService : GenericService<Message, MessageDTO>
    {
        public MessageService(IMapper mapper) : base(mapper) {  }

        public async Task<List<MessageDTO>> GetConvMsgs(string convId)
        {
            var fiveMinutesAgo = DateTime.UtcNow.AddMinutes(-10);

            var messages = await _collection.Find(x => x.ConversationId.Equals(convId) && x.Timestamp >= fiveMinutesAgo)
                                           .ToListAsync();

            return _mapper.Map<List<MessageDTO>>(messages);
        }


    }
}