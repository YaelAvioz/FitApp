using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using FitAppServer.Services;

namespace FitAppServer.Model
{
    public class User : GenericEntity
    {
        public virtual string username { get; set; }
        public virtual byte[] passwordHash { get; set; }
        public virtual byte[] passwordSalt { get; set; }
        public string token { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public int age { get; set; }

        public int height { get; set; }

        // weight = [(55.3, "2.4.23"), (55.9, "30.3.23")]
        public List<Tuple<double, DateTime>> weight { get; set; }

        public string gender { get; set; }

        public double bmi { get; set; }

        public string goal { get; set; }

        public string mentor { get; set; }

        public List<string> tags { get; set; }

        // water = [([T, T, F, F, T], "2.4.23"), ([F, F, F, F, F], "3.4.23")]
        public List<Tuple<List<bool>, DateTime>> water { get; set; }

        // foods = [(food_id, 50g, "2.4.23"), (food_id, 147g, "30.3.23")]
        public List<Tuple<string, double, DateTime>> foods { get; set; }

        public string GetChat()
        {
            string tag = "";
            foreach (string t in tags) { tag += t; tag += " "; }

            return "the client (" + firstname + ") is a " + gender + " at the age of " + age.ToString() +
                    ". Height (cm):" + height.ToString() + ". Curren weight(kg):" + weight[weight.Count - 1].Item1.ToString() +
                    ". His goal:" + goal + ". His interests are:" + tag + ".The client ate recently:";
        }

        public string FirstMsg()
        {
            return "Hi " + firstname + "! I am so happy that you joined us. My name is "
                + mentor + " and I will be your mentor. Feel free to talk to me every time" +
                " you have a doubt. I am here for you !";
        }

        // BMI = kg/m2
        public double getBmi()
        {
            if (this.bmi == 0)
            {
                double heightInMeters = this.height / 100.0; // Convert height from cm to m
                this.bmi = weight[-1].Item1 / Math.Pow(heightInMeters, 2);
            }

            return this.bmi;
        }

        public double getBmi(double weightAtRegistration)
        {
            if (this.bmi == 0)
            {
                double heightInMeters = this.height / 100.0; // Convert height from cm to m
                double temp = weightAtRegistration / Math.Pow(heightInMeters, 2);
                this.bmi = Math.Round(temp, 2);
            }

            return this.bmi;
        }

        public int GetWaterRecommendation()
        {
            // Daily water intake (liters) = 0.033 x Body weight (kg)
            var liters = 0.033 * weight[weight.Count - 1].Item1;
            // Divide to get in cups
            return (int)Math.Ceiling(liters * 4.2267528377);
        }

        public void AddWater(int capsToAdd)
        {
            if (capsToAdd == 0)
                return;

            int days = water.Count;

            if (water[days - 1].Item2.Date != DateTime.Now.Date)
            {
                Tuple<List<bool>, DateTime> newElement = new Tuple<List<bool>, DateTime>(new List<bool>(), DateTime.Today);
                water.Add(newElement);
                
                days += 1;
            }
            
            int firstFalse = water[days - 1].Item1.FindLastIndex(b => b == true) + 1;

            // add water
            if (capsToAdd > 0)
            {
                for (int i = 0; i < capsToAdd; i++)
                {
                    water[days - 1].Item1[firstFalse] = true;
                    firstFalse += 1;
                }
            }

            // remove water
            else
            {
                for (int i = 0; i < Math.Abs(capsToAdd); i++)
                {
                    int todays = water[days - 1].Item1.Count;
                    if (todays > 0)
                    {
                        firstFalse -= 1;
                        water[days - 1].Item1[firstFalse] = false;
                    }
                }
            }

        }
    }
}