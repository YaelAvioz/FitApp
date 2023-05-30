using FitAppServer.Model;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FitAppServer.DTO
{
    public class AddFoodDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string FoodId { get; set; }
        public virtual double Amount { get; set; }
    }
}