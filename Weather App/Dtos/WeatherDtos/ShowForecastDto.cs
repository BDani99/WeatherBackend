namespace Weather_App.Dtos
{
    public class ShowForecastDto
    {
        public DateTime Date { get; set; }
        public float MinTemp { get; set; }
        public float MaxTemp { get; set; }
        public string Main { get; set; } = null!;
        public float Wind { get; set; }
        public float FeelsLike { get; set; }
        public float Humidity { get; set; }
        public float Temp { get; set; }
        public string Icon { get; set; } = null!;
    }
}
