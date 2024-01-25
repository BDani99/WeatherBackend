using AutoMapper;
using Secret_Sharing_Platform.Dtos;
using Weather_App.Dtos;
using Weather_App.Dtos.WeatherDtos;
using Weather_App.Helpers;
using Weather_App.Models;

namespace Weather_App.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, RegisterDto>();
            CreateMap<UserDto, SettingDto>();
            CreateMap<User, UserDto>();
            CreateMap<List<Setting>, UserDto>()
                .ForMember(dest => dest.UnitOfMeasure,
                opt => opt.MapFrom(src => src.First(x => x.Key == KeyConstans.UNIT_OF_MEASURE).Value))
                .ForMember(dest => dest.City,
                opt => opt.MapFrom(src => src.First(x => x.Key == KeyConstans.CITY).Value))
                .ForMember(dest => dest.DefaultPage,
                opt => opt.MapFrom(src => src.First(x => x.Key == KeyConstans.DEFAULT_PAGE).Value));

            CreateMap<UserMobileDto, UserDto>().
                ForMember(src => src.DefaultPage, opt => opt.MapFrom(x => "Default"));

            CreateMap<ForecastDto, ShowForecastDto>()
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Weather.First().Main))
                .ForMember(dest => dest.Temp, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.Wind, opt => opt.MapFrom(src => src.Wind.Speed))
                .ForMember(dest => dest.FeelsLike, opt => opt.MapFrom(src => src.Main.Feels_Like))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Main.Humidity))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Datetime))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Weather.First().Icon))
                .ForMember(dest => dest.MinTemp, opt => opt.MapFrom(src => src.Main.Temp_Min))
                .ForMember(dest => dest.MaxTemp, opt => opt.MapFrom(src => src.Main.Temp_Max));

            CreateMap<WeatherDto, ShowWeatherDto>()
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Weather.First().Main))
                .ForMember(dest => dest.Temp, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.Wind, opt => opt.MapFrom(src => src.Wind.Speed))
                .ForMember(dest => dest.FeelsLike, opt => opt.MapFrom(src => src.Main.Feels_Like))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Main.Humidity))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Dt)))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Weather.First().Icon));
        }
    }
}
