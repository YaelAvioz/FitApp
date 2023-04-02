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
        [Route("{id}/serving/{amount}/{serving}")]
        public async Task<ActionResult<FoodDTO>> GetFoodByServing(string id, string amount, string serving)
        {
            FoodDTO food = await _foodService.GetFoodInfoByServing(id, amount, serving);
            
            if (food == null)
            {
                return NotFound();
            } 
            else
            {
                return Ok(food);
            }
        }

        [HttpGet]
        [Route("{id}/protein")]
        public async Task<ActionResult<string>> GetFoodProtein(string id)
        {
            FoodDTO food = await _foodService.Get(id);

            if (food == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(food.protein);
            }
        }

        [HttpGet]
        [Route("{id}/fat")]
        public async Task<ActionResult<string>> GetFoodFat(string id)
        {
            FoodDTO food = await _foodService.Get(id);

            if (food == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(food.fat);
            }
        }

        [HttpGet]
        [Route("{id}/carbs")]
        public async Task<ActionResult<string>> GetFoodCarbs(string id)
        {
            FoodDTO food = await _foodService.Get(id);

            if (food == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(food.carbohydrate);
            }
        }
    }
}