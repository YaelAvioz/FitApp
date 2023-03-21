namespace FitAppServer.Model
{
    public class Food : GenericEntity
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
        
        // we have 100g. user asks for 15 grams. calculate - 15*values/100
        public virtual Food FoodByGrams(int g)
        {
            return null;
        }

        public virtual Food FoodBy100Grams(int g100)
        {
            return null;

        }

    }
}
