namespace Secret_Sharing_Platform.Helper
{
    public class JwtOptions
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public int ExpiresInDays { get; set; }
    }
}
