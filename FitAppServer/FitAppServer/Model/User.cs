using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

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

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> foods { get; set; }

        public string getChat()
        {
            string tag = "";
            foreach (string t in tags) { tag += t; }
            string food = "";
            foreach (string f in foods) { food += f; }

            return "the client (" + firstname + ") is a " + gender + " at the age of " + age.ToString() +
                    ". Height (cm): " + height.ToString() + ". Curren weight (kg): " + weight[weight.Count - 1].Item1.ToString() +
                    ". His goal: " + goal + ". His interests are: " + tag + ". The client ate recently: " + food + ".";
        }

        public string FirstMsg()
        {
            return "Hi " + firstname + "! I am so happy that you joined us. My name is "
                + mentor + " and I will be your mentor. Feel free to talk to me every time" +
                " you have a doubt. I am here for you";
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

    }
}