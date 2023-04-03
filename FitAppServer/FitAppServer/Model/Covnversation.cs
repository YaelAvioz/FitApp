using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FitAppServer.Model
{
    public class Conversation : GenericEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public List<MessageDTO> Messages { get; set; }
    }
}
