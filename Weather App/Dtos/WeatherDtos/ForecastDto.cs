namespace Weather_App.Dtos.WeatherDtos
{
    public class ForecastDto
    {
        public MainWithMinAndMaxTempDto Main { get; set; } = null!;
        public List<AttributeDto> Weather { get; set; } = null!;
        public WindDto Wind { get; set; } = null!;
        public string Dt_Txt { get; set; } = null!;
        public DateTime Datetime { get; set; }
    }
}
