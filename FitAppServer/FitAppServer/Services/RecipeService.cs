using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;

namespace FitAppServer.Services
{
    public class RecipeService : GenericService<Recipe, RecipeDTO>
    {
        public RecipeService(IMapper mapper) : base(mapper) {  }
    }
}