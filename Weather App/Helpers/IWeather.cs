using Refit;
using Weather_App.Dtos;

namespace Weather_App.Helpers
{
    public interface IWeather
    {
        [Get("/data/2.5/weather?q={city}&appid=17962c88c64adfcdd6e06988b2c944cf&units=metric")]
        Task<WeatherDto> GetWeather(string city);
    }
}
