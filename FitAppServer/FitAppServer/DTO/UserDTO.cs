using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace FitAppServer.DTO
{
    public class UserDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string token { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public int age { get; set; }

        public int height { get; set; }

        // weight = [(55.3, "2.4.23"), (55.9, "30.3.23")]
        public List<Tuple<double, DateTime>> weight { get; set; }

        public string gender { get; set; }

        public double bmi { get; set; }

        public string goal { get; set; }

        public string mentor { get; set; }

        public List<string> tags { get; set; }

        // water = [([T, T, F, F, T], "2.4.23"), ([F, F, F, F, F], "3.4.23")]
        public List<Tuple<List<bool>, DateTime>> water { get; set; }

        // foods = [(food_id, 50g, "2.4.23"), (food_id, 147g, "30.3.23")]
        public List<Tuple<string, double, DateTime>> foods { get; set; }

        public string firstMsg { get; set; }
    }
}
