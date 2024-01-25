namespace Weather_App.Dtos
{
    public class ShowWeatherDto
    {
        public string Main { get; set; } = null!;
        public float Temp { get; set; }
        public float Wind { get; set; }
        public float FeelsLike { get; set; }
        public float Humidity { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Icon { get; set; } = null!;
    }
}
