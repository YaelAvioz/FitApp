using FitAppServer.Model;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace FitAppServer.DTO
{
    public class ConversationDTO : GenericEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public List<MessageDTO> Messages { get; set; }
    }
}
