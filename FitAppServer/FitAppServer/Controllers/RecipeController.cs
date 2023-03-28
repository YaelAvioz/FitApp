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
        private static RecipeIMGService _recipeIMGService;


        public RecipeController(IMapper mapper) : base(mapper) 
        {
            _recipeService = new RecipeService(mapper);
            _recipeIMGService = new RecipeIMGService();
        }

        [HttpGet]
        [Route("search/{query}")]
        public ActionResult<List<RecipeDTO>> GetRecipesByQuery([FromRoute] string query)
        {
            var recipes = _recipeService.GetRecipesByQuery(query);

            if (recipes == null)
            {
                return NotFound();
            }
            else
            {
                recipes = _recipeIMGService.GetImgs(recipes);
            }

            return Ok(recipes);
        }
    }
}