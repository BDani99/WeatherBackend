using System.ComponentModel.DataAnnotations;

namespace Weather_App.Dtos
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        [MinLength(8)]
        public string Password { get; set; } = null!;
    }
}
