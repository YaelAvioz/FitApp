using FitAppServer.Services;
using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FitAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : GenericController<Food, FoodDTO>
    {
        private static FoodService _foodService;
        public FoodController(IMapper mapper) : base(mapper)
        {
            _foodService = new FoodService(mapper);
        }

        [HttpGet]
        [Route("{id}/serving/{amount}")]
        public async Task<ActionResult<FoodDTO>> GetFoodByAmount(string id, string amount)
        {
            double doubleAmount = Double.Parse(amount);
            FoodDTO food = await _foodService.GetFoodInfoByAmount(id, doubleAmount);

            if (food == null) return NotFound();

            return Ok(food);
        }

        [HttpGet]
        [Route("search/{query}/{next}")]
        public ActionResult<List<FoodDTO>> GetFoodByQuery([FromRoute] string query, [FromRoute] string next)
        {
            var food = _foodService.GetFoodByQuery(query, int.Parse(next));

            if (food == null) return NotFound();
            
            return Ok(food);
        }

        [HttpGet]
        [Route("search/{query}/count")]
        public ActionResult<int> GetFoodCountByQuery([FromRoute] string query)
        {
            var length = _foodService.GetFoodCountByQuery(query);

            if (length == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(length.Value);
            }
        }

        [HttpGet]
        [Route("{id}/protein")]
        public async Task<ActionResult<string>> GetFoodProtein(string id)
        {
            FoodDTO food = await _foodService.Get(id);

            if (food == null) return NotFound();

            return Ok(food.protein);
        }

        [HttpGet]
        [Route("{id}/fat")]
        public async Task<ActionResult<string>> GetFoodFat(string id)
        {
            FoodDTO food = await _foodService.Get(id);

            if (food == null) return NotFound();

            return Ok(food.fat);
        }

        [HttpGet]
        [Route("{id}/carbs")]
        public async Task<ActionResult<string>> GetFoodCarbs(string id)
        {
            FoodDTO food = await _foodService.Get(id);

            if (food == null) return NotFound();
            
            return Ok(food.carbohydrate);
        }
    }
}