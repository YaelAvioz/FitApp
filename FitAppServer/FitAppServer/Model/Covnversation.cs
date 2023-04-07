using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using FitAppServer.DTO;

namespace FitAppServer.Model
{
    public class Conversation : GenericEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Messages { get; set; }
    }
}
