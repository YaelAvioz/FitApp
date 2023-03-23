using System.ComponentModel.DataAnnotations;

namespace FitAppServer.DTO
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
