using Refit;
using Weather_App.Dtos;

namespace Weather_App.Helpers
{
    public interface IForecast
    {
        [Get("/data/2.5/forecast?q={city}&appid=17962c88c64adfcdd6e06988b2c944cf&units=metric")]
        Task<ForecastDtos> GetForecasts(string city);
    }
}
    