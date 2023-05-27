using FitAppServer.DTO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static MongoDB.Driver.WriteConcern;

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
        public virtual Food FoodByGrams(double grams)
        {
            Food newFood = new Food
            {
                Id = null,
                name = this.name,
                serving_size = grams + " g",
                calories = ConvertByGram(grams, this.calories),
                total_fat = ConvertByGram(grams, this.total_fat),
                calcium = ConvertByGram(grams, this.calcium),
                protein = ConvertByGram(grams, this.protein),
                carbohydrate = ConvertByGram(grams, this.carbohydrate),
                fiber = ConvertByGram(grams, this.fiber),
                sugars = ConvertByGram(grams, this.sugars),
                fat = ConvertByGram(grams, this.fat)
            };

            return newFood;
        }


        private float ExtractFloatValue(string input)
        {
            float result;

            if (float.TryParse(Regex.Match(input, @"\d+(\.\d+)?").Value, out result))
            {
                return result;
            }
            throw new ArgumentException("Input string does not contain a valid float value.");
        }

        private string ExtractUnit(string input)
        {
            string pattern = @"[^\d.]+";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                return " " + match.Value.Trim();
            }

            return "";
        }

        private string ConvertByGram(double grams, string per100grams)
        {
            var per100grams_clean = ExtractFloatValue(per100grams);

            if (per100grams_clean == 0.0)
            {
                return "0.0".ToString();
            }
            var res = (grams / 100.0) * per100grams_clean;
            return Math.Round(res, 3) + ExtractUnit(per100grams);
        }

    }
}
