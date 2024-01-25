using System.ComponentModel.DataAnnotations;

namespace Secret_Sharing_Platform.Dtos
{
    public class RegisterDto
    {
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [MinLength(8)]
        public string Password { get; set; } = null!;
    }
}
