using System;

namespace Secret_Sharing_Platform.Dto
{
    public class TokenDto
    {
        public string? Token { get; set; }
        public DateTimeOffset Expires { get; set; }
    }
}
