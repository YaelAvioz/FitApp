using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;
using MongoDB.Bson;

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
                .Skip(skip).Limit(20).ToList());
        }

        public async Task<FoodDTO> GetFoodInfoByServing(string id, string amount, string serving)
        {
            var food = await _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (food != null)
            {
                if (food.serving_size.Equals(serving))
                {
                    return _mapper.Map<FoodDTO>(food);
                }
                else
                {
                    switch (serving)
                    {
                        case "g":
                            return _mapper.Map<FoodDTO>(food.FoodByGrams(amount));
                        case "kg":
                            return _mapper.Map<FoodDTO>(food.FoodBy100Grams(amount));
                    }
                }
            }
            return null;
        }

    }
}