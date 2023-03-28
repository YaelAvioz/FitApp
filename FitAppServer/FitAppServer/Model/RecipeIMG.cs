using MongoDB.Libmongocrypt;

namespace FitAppServer.Model
{
    public class RecipeIMG : GenericEntity
    {
        public virtual Binary binary { get; set; }
        public virtual string imageName { get; set; }
    }
}