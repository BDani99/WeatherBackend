using AutoMapper;
using Microsoft.Extensions.Options;
using Secret_Sharing_Platform.Dtos;
using System.Security.Claims;
using Weather_App.Dtos;
using Weather_App.Helpers;
using Weather_App.Models;
using Weather_App.Repository;

namespace Weather_App.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly SettingDto _defaultSettings;

        public UserService(IUserRepository userRepository, IMapper mapper, 
                            IOptions<SettingDto> options)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _defaultSettings = options.Value;
        }

        public async Task<User> CreateUserAsync(RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            user = await _userRepository.CreateUserAsnyc(user);

            var settings = CreateSettings(user.Id, _defaultSettings.UnitOfMeasure,
                                        _defaultSettings.City, _defaultSettings.DefaultPage);

            await _userRepository.CreateSettingAsync(settings);
            return user;
        }

        public int? GetUserId(ClaimsPrincipal User)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null) return null;
            return int.Parse(identity.Claims
                .First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                .Value);
        }

        public async Task UpdateSettingsAsync(SettingDto settingDto, int userId)
        {
            var settings = CreateSettings(userId, settingDto.UnitOfMeasure,
                                            settingDto.City, settingDto.DefaultPage);

            await _userRepository.UpdateSettingsAsync(settings, userId);
        }

        public Task<Setting?> GetDefaultPageByUserIdAsync(int userId)
        {
            return _userRepository.GetDefaultPageByUserIdAsync(userId);
        }

        private List<Setting> CreateSettings(int userId, string unitOfMeasure, 
                                            string city, string defaultPage)
        {
            var unitOfMeasureSetting = new Setting
            {
                UserId = userId,
                Key = KeyConstans.UNIT_OF_MEASURE,
                Value = unitOfMeasure
            };
            var citySetting = new Setting
            {
                UserId = userId,
                Key = KeyConstans.CITY,
                Value = city
            };
            var defaultPageSetting = new Setting
            {
                UserId = userId,
                Key = KeyConstans.DEFAULT_PAGE,
                Value = defaultPage
            };
            return new List<Setting> { unitOfMeasureSetting, citySetting, defaultPageSetting };
        }

        public async Task UpdateUserAsync(UserDto userDto, int userId)
        {
            await _userRepository.ChangeUsernameByIdAsync(userId, userDto.Username);
            await UpdateSettingsAsync(_mapper.Map<SettingDto>(userDto),userId);
        }

        public async Task UpdateUserMobileAsync(UserMobileDto userMobileDto, int userId)
        {
            var userDto = _mapper.Map<UserDto>(userMobileDto);
            await _userRepository.ChangeUsernameByIdAsync(userId, userMobileDto.Username);
            await UpdateSettingsAsync(_mapper.Map<SettingDto>(userDto), userId);
        }

        public async Task<UserDto?> GetUsernameAndSettingsByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) { return null; }

            var userSettings = await _userRepository.GetUserSettingByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(userSettings);
            userDto.Username = user.Name;
            return userDto;
        }
    }
}
