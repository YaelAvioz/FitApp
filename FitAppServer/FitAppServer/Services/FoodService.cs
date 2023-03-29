using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;

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

        /*public async Task<Food> GetFoodInfoByServing(string foodName, string count, string serving)
        {
            var filter = Builders<Food>.Filter.Eq(x => x.name, foodName);
            var foods = await _collection.Find(filter).ToListAsync();

            if (foods.Any())
            {
                Food food = foods.First();
                if (food.serving_size.Equals(serving))
                {
                    return food;
                }
                else
                {
                    switch (serving)
                    {
                        case "g":
                            return food.FoodByGrams(int.Parse(count));
                        case "kg":
                            return food.FoodBy100Grams(int.Parse(count));
                            


                    }
                }

            }
        }*/

    }
}