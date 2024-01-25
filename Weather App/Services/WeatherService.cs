using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Refit;
using System.Text.Json;
using Weather_App.Dtos;
using Weather_App.Helpers;
using Weather_App.Repository;

namespace Weather_App.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public WeatherService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ShowWeatherDto?> ShowWeatherAsync(string? city, int userId)
        {
            var weatherClient = RestService.For<IWeather>("http://api.openweathermap.org");
            if (city is null)
            {
                var citySetting = await _userRepository.GetCurrentUserCityAsync(userId);
                if (citySetting is null) return null;
                city = citySetting.Value;
            }

            var weatherDto = await weatherClient.GetWeather(city);
            if (weatherDto is null) return null;

            var measureSetting = await _userRepository.GetUnitOfMeasureAsync(userId);
            if (measureSetting is null) return null;

            var unitOfMeasure = measureSetting.Value;
            if (unitOfMeasure == "F")
            {
                //Celsius to fahrenheit
                weatherDto.Main.Temp = (weatherDto.Main.Temp * 9) / 5 + 32;
            }

            var showWeatherDto = _mapper.Map<ShowWeatherDto>(weatherDto);

            return showWeatherDto;
        }

        public async Task<IEnumerable<ShowForecastDto>?> ShowForecastAsync(string? city, int userId)
        {
            var forecastClient = RestService.For<IForecast>("http://api.openweathermap.org");
            if (city is null)
            {
                var citySetting = await _userRepository.GetCurrentUserCityAsync(userId);
                if (citySetting is null) return null;
                city = citySetting.Value;
            }

            var forecastDtos = await forecastClient.GetForecasts(city);
            if (forecastDtos is null) return null;

            foreach (var forecastDto in forecastDtos.List)
            {
                forecastDto.Datetime = DateTime.ParseExact(forecastDto.Dt_Txt,
                    "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);
            }

            return forecastDtos.List.Select(forecastDto => _mapper.Map<ShowForecastDto>(forecastDto));
        }
    }
}
