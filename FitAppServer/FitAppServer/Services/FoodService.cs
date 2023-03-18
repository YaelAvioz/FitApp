using fitappserver.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;

namespace fitappserver.Services
{
    public class FoodService : GenericService<Food, FoodDTO>
    {
        private readonly IMongoCollection<Food> _collection;

        public FoodService(IMapper mapper) : base(mapper) {  }

        public async Task<Food> GetDefaultFoodInfo(string foodName)
        {
            var filter = Builders<Food>.Filter.Eq(x => x.FoodName, foodName);
            var foods = await _collection.Find(filter).ToListAsync();
            return foods.FirstOrDefault();
        }
    }
}

/*

public Food GetFoodInfoByServing(string foodName, string count, string serving)
{
    List<Food> foods = _entities.FindAll(food => food.FoodName.Equals(foodName));
    if (foods.Any())
    {
        Food food = foods.First();
        if (food.ServingSize.Equals(serving))
        {
            return food;
        }
        else
        {
            switch (serving)
            {
                case "g":
                    break;
                case "kg":
                    break;

            }
        }

    }
}

public String GetCaloriesPerServing(string foodName)
{
    List<Food> foods = _entities.FindAll(food => food.FoodName.Equals(foodName));
    return foods.Any() ? foods.First().Calories : "0";
}*/