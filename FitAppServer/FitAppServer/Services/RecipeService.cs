using FitAppServer.Model;
using AutoMapper;
using FitAppServer.Helper;
using FitAppServer.DTO;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitAppServer.Services
{
    public class RecipeService : GenericService<Recipe, RecipeDTO>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        static List<string> _weekFavorits = new List<string>();

        public RecipeService(IMapper mapper) : base(mapper) {  }

        public List<RecipeCardDTO> GetRecipesByQuery(string query)
        {
            var filter = Builders<Recipe>.Filter.Regex(x => x.Title, new BsonRegularExpression(query, "i"));
            
            return _mapper.Map<List<RecipeCardDTO>>(_collection.Find(filter).ToList());
        }

        public List<RecipeDTO> GetSingleRecipe(string query)
        {
            var filter = Builders<Recipe>.Filter.Regex(x => x.Title, new BsonRegularExpression(query, "i"));

            return _mapper.Map<List<RecipeDTO>>(_collection.Find(filter).ToList());
        }


        public List<RecipeCardDTO> GetWeekFavorite()
        {
            if (_weekFavorits.Count == 0)
            {
                _weekFavorits = Util.GetRandom();
            }
            var filter = Builders<Recipe>.Filter.In(x => x.Id, _weekFavorits);
            return _mapper.Map<List<RecipeCardDTO>>(_collection.Find(filter).ToList());
        }
    }
}