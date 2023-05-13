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
            _recipeService = new RecipeService(mapper);
        }

        [HttpGet]
        [Route("search/{query}/{next}")]
        public ActionResult<List<RecipeCardDTO>> GetRecipesByQuery([FromRoute] string query, [FromRoute] string next)
        {
            var recipes = _recipeService.GetRecipesByQuery(query, int.Parse(next));

            if (recipes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(recipes);
            }
        }

        [HttpGet]
        [Route("singleRecipe/{query}")]
        public ActionResult<RecipeDTO> GetSingleRecipe([FromRoute] string query)
        {
            var recipe = _recipeService.GetSingleRecipe(query);

            if (recipe == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(recipe);
            }
        }


        [HttpGet]
        [Route("weekFavorite")]
        public ActionResult<List<RecipeCardDTO>> GetWeekFavorite()
        {
            var recipes = _recipeService.GetWeekFavorite();

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