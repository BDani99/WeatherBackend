using Weather_App.Dtos.WeatherDtos;

namespace Weather_App.Dtos
{
    public class WeatherDto
    {
        public List<AttributeDto> Weather { get; set; } = null!;
        public MainDto Main { get; set; } = null!;
        public WindDto Wind { get; set; } = null!;
        public long Dt { get; set; }
    }
}
