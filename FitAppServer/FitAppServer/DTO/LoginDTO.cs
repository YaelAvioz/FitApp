using System.ComponentModel.DataAnnotations;

namespace FitAppServer.DTO
{
    public class LoginDTO
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
