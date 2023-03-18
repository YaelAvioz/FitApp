namespace fitappserver.Model
{
    public class Food : GenericEntity
    {
        public virtual string FoodName { get; set; }
        public virtual string ServingSize { get; set; }
        public virtual string Calories { get; set; }
        public virtual string TotalFat { get; set; }
        public virtual string Cholesterol { get; set; }
        public virtual string VitaminA { get; set; }
        public virtual string VitaminB12 { get; set; }
        public virtual string VitaminC { get; set; }
        public virtual string VitaminD { get; set; }
        public virtual string VitaminE { get; set; }
        public virtual string VitaminK { get; set; }
        public virtual string Calcium { get; set; }
        public virtual string Magnesium { get; set; }
        public virtual string Protein { get; set; }
        public virtual string Carbohydrate { get; set; }
        public virtual string Fiber { get; set; }
        public virtual string Sugars { get; set; }
        public virtual string Glucose { get; set; }
        public virtual string Lactose { get; set; }
        public virtual string Fat { get; set; }
        public virtual string Caffeine { get; set; }
    }
}
