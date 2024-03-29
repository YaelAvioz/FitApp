﻿using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text;
using System.Collections.Generic;

namespace FitAppServer.Services
{
    public class FoodService : GenericService<Food, FoodDTO>
    {
        public FoodService(IMapper mapper) : base(mapper) {  }

        public async Task<Food> GetDefaultFoodInfo(string foodName)
        {
            var filter = Builders<Food>.Filter.Eq(x => x.name, foodName);
            var foods = await _collection.Find(filter).ToListAsync();
            return foods.FirstOrDefault();
        }

        public async Task<string> GetCaloriesPerDefaultServing(string foodName)
        {
            var filter = Builders<Food>.Filter.Eq(x => x.name, foodName);
            var foods = await _collection.Find(filter).ToListAsync();
            return foods.FirstOrDefault().calories;
        }

        public List<FoodDTO> GetFoodByQuery(string query, int skip)
        {
            var filter = Builders<Food>.Filter.Regex(x => x.name, new BsonRegularExpression(query, "i"));

            return _mapper.Map<List<FoodDTO>>(_collection.Find(filter)
                .Skip(skip).Limit(15).ToList());
        }

        public int? GetFoodCountByQuery(string query)
        {
            var filter = Builders<Food>.Filter.Regex(x => x.name, new BsonRegularExpression(query, "i"));

            return Convert.ToInt32(_collection.Find(filter).CountDocuments());
        }

        public async Task<FoodDTO> GetFoodInfoByAmount(string id, double amount)
        {
            var food = await _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (food != null)
            {
                if (food.serving_size.Equals(amount))
                {
                    return _mapper.Map<FoodDTO>(food);
                }
                
                return _mapper.Map<FoodDTO>(food.FoodByGrams(amount));
            }
            return null;
        }

        public async Task<string> GetFoodForPrompt(List<Tuple<string, double>> foods)
        {
            StringBuilder promptBuilder = new StringBuilder();

            foreach (var foodTuple in foods)
            {
                string foodId = foodTuple.Item1;
                double amount = foodTuple.Item2;

                var food = await GetFoodInfoByAmount(foodId, amount);

                if (food != null)
                {
                    string foodInfo = $"Food: {food.name}, Amount: {amount} grams, Calories: {food.calories} kcal\n";
                    promptBuilder.Append(foodInfo);
                }
            }

            return promptBuilder.ToString();
        }

        public async Task<List<FoodDTO>> GetFoods(List<Tuple<string, double>> ingredients)
        {
            var ingredientsFoods = new List<FoodDTO>();

            foreach (Tuple<string, double> ingredient in ingredients)
            {
                var filter = Builders<Food>.Filter.Regex(x => x.name, new MongoDB.Bson.BsonRegularExpression(ingredient.Item1, "i"));
                var foods = await _collection.Find(filter).ToListAsync();
                var food = foods.FirstOrDefault(f => f.name.Contains(ingredient.Item1, StringComparison.OrdinalIgnoreCase));


                if (food != null)
                {
                    FoodDTO foodByAmount = await GetFoodInfoByAmount(food.Id, ingredient.Item2);
                    ingredientsFoods.Add(foodByAmount);
                }
            }
            return ingredientsFoods;
        }
    }
}