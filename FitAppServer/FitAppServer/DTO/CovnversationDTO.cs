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

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Messages { get; set; }
    }
}
