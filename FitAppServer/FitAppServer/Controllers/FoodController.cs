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
        [Route("{foodName}")]
        public async Task<ActionResult<Food>> GetFood([FromRoute] string foodName)
        {
            Food food = await _foodService.GetDefaultFoodInfo(foodName);
            if (food == null)
            {
                return NotFound();
            }
            return Ok(food);
        }
    }
}