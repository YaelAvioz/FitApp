using FitAppServer.Model;

namespace FitAppServer.DTO
{
    public class RecipeCardDTO : GenericEntity
    {
        public virtual string Title { get; set; }
        public virtual string Url { get; set; }
    }
}
