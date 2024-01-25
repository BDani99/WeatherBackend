using Weather_App.Dtos.WeatherDtos;

namespace Weather_App.Dtos
{
    public class ForecastDtos
    {
        public IEnumerable<ForecastDto> List { get; set; } = new List<ForecastDto>();
    }
}
