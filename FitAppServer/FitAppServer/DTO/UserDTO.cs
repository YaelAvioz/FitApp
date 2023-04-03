using System.ComponentModel.DataAnnotations;

namespace FitAppServer.DTO
{
    public class UserDTO
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string token { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public int age { get; set; }

        public int height { get; set; }

        // weight = [(55.3, "2.4.23"), (55.9, "30.3.23")]
        public List<Tuple<float, string>> weight { get; set; }

        public string gender { get; set; }

        public float bmi { get; set; }

        public string goal { get; set; }

        public string mentor { get; set; }

        public List<string> tags { get; set; }

        public List<FoodDTO> foods { get; set; }
    
        public string UserProfile()
        {
            return "the client is a " + gender + " at the age of " + age.ToString() +
                ". The client's height is " + height.ToString() +
                " and curren weight is " + weight[-1].Item1.ToString() + ". " +
                "The client's goal is: " + goal + ". and the tags the client is" +
                " intrested in are: " + tags.ToString() + ". Last thing you need to know is the food " +
                "the client ate recently: " + foods.ToString() + ".";
        }
    }
}
