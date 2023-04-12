using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FitAppServer.Services
{
    public class RecipeService : GenericService<Recipe, RecipeDTO>
    {
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
    }
}