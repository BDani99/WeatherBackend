using Weather_App.Dtos;

namespace Weather_App.Service
{
    public interface IWeatherService
    {
        Task<IEnumerable<ShowForecastDto>?> ShowForecastAsync(string? city, int userId);
        Task<ShowWeatherDto?> ShowWeatherAsync(string? city, int userId);
    }
}