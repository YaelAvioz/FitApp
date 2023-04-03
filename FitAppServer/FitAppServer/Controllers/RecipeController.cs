using FitAppServer.Services;
using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FitAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : GenericController<Recipe, RecipeDTO>
    {
        private static RecipeService _recipeService;

        public RecipeController(IMapper mapper) : base(mapper) 
        {
            _recipeService = new RecipeService(mapper);        }

        [HttpGet]
        [Route("search/{query}")]
        public ActionResult<List<RecipeCardDTO>> GetRecipesByQuery([FromRoute] string query)
        {
            var recipes = _recipeService.GetRecipesByQuery(query);

            if (recipes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(recipes);
            }
        }
    }
}