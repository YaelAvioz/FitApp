using FitAppServer.Model;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FitAppServer.DTO
{
    public class MessageDTO : GenericEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ConversationId { get; set; }

        // True for user to mentor, False for mentor to user
        public bool IsFromUser { get; set; }

        public string Content { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
