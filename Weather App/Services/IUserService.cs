using Secret_Sharing_Platform.Dtos;
using System.Security.Claims;
using System.Threading.Tasks;
using Weather_App.Dtos;
using Weather_App.Models;

namespace Weather_App.Service
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(RegisterDto registerDto);
        int? GetUserId(ClaimsPrincipal User);
        Task UpdateSettingsAsync(SettingDto settingDto, int userId);
        Task<Setting?> GetDefaultPageByUserIdAsync(int userId);
        Task UpdateUserAsync(UserDto userDto, int userId);
        Task UpdateUserMobileAsync(UserMobileDto userMobileDto, int userId);
        Task<UserDto?> GetUsernameAndSettingsByIdAsync(int id);
    }
}