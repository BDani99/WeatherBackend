using Weather_App.Models;

namespace Weather_App.Repository
{
    public interface IUserRepository
    {
        bool AlreadyUserWithThisEmail(string email);
        Task<User> CreateUserAsnyc(User user);
        Task<List<Setting>> CreateSettingAsync(List<Setting> settings);
        Task<User?> GetUserByEmailAsync(string email);
        Task<Setting?> GetUnitOfMeasureAsync(int userId);
        Task<Setting?> GetCurrentUserCityAsync(int userId);
        Task UpdateSettingsAsync(List<Setting> settings, int userId);
        Task<Setting?> GetDefaultPageByUserIdAsync(int userId);
        Task<User> ChangeUsernameByIdAsync(int id, string username);
        Task<User?> GetUserByIdAsync(int id);
        Task<List<Setting>> GetUserSettingByIdAsync(int userId);
    }
}