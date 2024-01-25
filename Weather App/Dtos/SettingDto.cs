namespace Weather_App.Dtos
{
    public class SettingDto
    {
        public string UnitOfMeasure { get; set; } = null!;
        public string City { get; set; } = null!;
        public string DefaultPage { get; set; } = null!;
    }
}
