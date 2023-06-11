using FitAppServer.Model;
namespace FitAppServer.DTO
{
    public class DoubleFoodDTO : GenericEntity
    {
        public virtual double calories { get; set; }
        public virtual double total_fat { get; set; }
        public virtual double calcium { get; set; }
        public virtual double protein { get; set; }
        public virtual double carbohydrate { get; set; }
        public virtual double fiber { get; set; }
        public virtual double sugars { get; set; }
        public virtual double fat { get; set; }
    }
}