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

        public int hight { get; set; }

        // weight = [(55.3, "2.4.23"), (55.9, "30.3.23")]
        public List<Tuple<float, string>> weight { get; set; }

        public string gender { get; set; }

        public float bmi { get; set; }

        public string goal { get; set; }

        public string mentor { get; set; }

        public List<string> tags { get; set; }

        public List<FoodDTO> foods { get; set; }
    }
}
