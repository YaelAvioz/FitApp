using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;
using MongoDB.Bson;

public class RecipeIMGService
{
    protected readonly IMongoCollection<RecipeIMG> _collection;
    protected readonly IMongoDatabase _db;
    protected readonly IMapper _mapper;
    protected readonly string connectionString = "mongodb+srv://FitApp:FitAppYaelCoral@cluster0.hsylfut.mongodb.net/?retryWrites=true&w=majority";
    protected readonly string databaseName = "fitapp";
    protected readonly string collectionName = "recipeIMG";

    public RecipeIMGService()
    {
        var client = new MongoClient(connectionString);
        _db = client.GetDatabase(databaseName);
        _collection = _db.GetCollection<RecipeIMG>(collectionName);
    }

    public RecipeIMG GetImg(string recipeName)
    {
        var filter = Builders<RecipeIMG>.Filter.Eq(x => x.imageName, recipeName);
        return _collection.Find(filter).ToList().FirstOrDefault();
    }

    public List<RecipeDTO> GetImgs(List<Recipe> recipes)
    {
        List<RecipeDTO> recipeDTOs = new List<RecipeDTO>();

        foreach (Recipe recipe in recipes)
        {
            recipeDTOs.Add(new RecipeDTO
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Instructions = recipe.Instructions,
                Ingredients = recipe.Ingredients,
                Image_Name = recipe.Image_Name,
                Image = GetImg(recipe.Title).binary
            });
        }
        return recipeDTOs;
    }
}