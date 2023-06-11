using FitAppServer.Model;
namespace FitAppServer.DTO
{
    public class GradeDTO : GenericEntity
    {
        public virtual int    grade { get; set; }
        public virtual double calories { get; set; }
        public virtual double total_fat_diff { get; set; }
        public virtual double calcium_diff { get; set; }
        public virtual double protein_diff { get; set; }
        public virtual double carbohydrate_diff { get; set; }
        public virtual double fiber_diff { get; set; }
        public virtual double sugars_diff { get; set; }
        public virtual double fat_diff { get; set; }
    }
}