namespace Weather_App.Dtos
{
    public class UserDto
    {
        public string Username { get; set; } = null!;
        public string UnitOfMeasure { get; set; } = null!;
        public string City { get; set; } = null!;
        public string DefaultPage { get; set; } = null!;
    }
}
