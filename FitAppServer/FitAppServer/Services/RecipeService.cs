using FitAppServer.Model;
using AutoMapper;
using FitAppServer.Helper;
using FitAppServer.DTO;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static MongoDB.Driver.WriteConcern;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace FitAppServer.Services
{
    public class RecipeService : GenericService<Recipe, RecipeDTO>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        static List<string> _weekFavorits = new List<string>();

        public RecipeService(IMapper mapper) : base(mapper) {  }

        public List<RecipeCardDTO> GetRecipesByQuery(string query, int skip)
        {
            var filter = Builders<Recipe>.Filter.Regex(x => x.Title, new BsonRegularExpression(query, "i"));
            
            return _mapper.Map<List<RecipeCardDTO>>(_collection.Find(filter)
                .Skip(skip).Limit(12).ToList());
        }

        public int? GetRecipesCountByQuery(string query)
        {
            var filter = Builders<Recipe>.Filter.Regex(x => x.Title, new BsonRegularExpression(query, "i"));

            return Convert.ToInt32(_collection.Find(filter).CountDocuments());
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


        public async Task<List<Tuple<string, double>>> GetIngredients(string recipeId)
        {
            List<FoodDTO> foods = new List<FoodDTO>();
            var recipe = await _collection.Find(x => x.Id.Equals(recipeId)).FirstOrDefaultAsync();
            List<Tuple<string, double>> ingredients = ParseRecipeIngredients(recipe.Ingredients);

            return ingredients;
        }

        private List<Tuple<string, double>> ParseRecipeIngredients(string recipeString)
        {
            // Remove the surrounding square brackets and split the string into individual ingredients
            string[] ingredients = recipeString.Trim('[', ']').Split(',');

            List<Tuple<string, double>> result = new List<Tuple<string, double>>();
            foreach (string ingredient in ingredients)
            {
                // Extract the food name and amount from each ingredient
                string[] parts = ingredient.Trim('\'', ' ').Split(' ');

                if (parts.Length >= 2 && double.TryParse(parts[0], out double amount))
                {
                    string foodName = string.Join(" ", parts, 1, parts.Length - 1);
                    string[] words = foodName.Split(' ');
                    string food = words[words.Length - 1];
                    result.Add(Tuple.Create(food, amount));
                }
            }

            return result;
        }
    }
}