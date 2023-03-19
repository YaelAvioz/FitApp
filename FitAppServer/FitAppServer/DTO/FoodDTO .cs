using FitAppServer.Model;
namespace FitAppServer.DTO
{
    public class FoodDTO : GenericEntity
    {
        public virtual string name { get; set; }
        public virtual string serving_size { get; set; }
        public virtual string calories { get; set; }
        public virtual string total_fat { get; set; }
        public virtual string calcium { get; set; }
        public virtual string protein { get; set; }
        public virtual string carbohydrate { get; set; }
        public virtual string fiber { get; set; }
        public virtual string sugars { get; set; }
        public virtual string fat { get; set; }
    }
}