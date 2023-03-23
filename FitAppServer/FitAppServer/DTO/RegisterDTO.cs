using System.ComponentModel.DataAnnotations;

namespace FitAppServer.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
