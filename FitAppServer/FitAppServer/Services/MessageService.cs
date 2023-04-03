using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;

namespace FitAppServer.Services
{
    public class MessageService : GenericService<Message, MessageDTO>
    {
        public MessageService(IMapper mapper) : base(mapper) {  }

    }
}