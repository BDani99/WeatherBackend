using Microsoft.EntityFrameworkCore;
using Weather_App.Data;
using Weather_App.Helpers;
using Weather_App.Models;

namespace Weather_App.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AlreadyUserWithThisEmail(string email)
        {
            return !_context.Users.Any(u => u.Email == email);
        }

        public async Task<User> CreateUserAsnyc(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<Setting>> CreateSettingAsync(List<Setting> settings)
        {
            _context.Settings.AddRange(settings);
            await _context.SaveChangesAsync();
            return settings;
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<Setting?> GetUnitOfMeasureAsync(int userId)
        {
            return _context.Settings.FirstOrDefaultAsync(x => 
            x.UserId == userId && 
            x.Key == KeyConstans.UNIT_OF_MEASURE);    
        }

        public Task<Setting?> GetCurrentUserCityAsync(int userId)
        {
            return _context.Settings.FirstOrDefaultAsync(x => 
            x.UserId == userId && 
            x.Key == KeyConstans.CITY);
            
        }

        public async Task UpdateSettingsAsync(List<Setting> settings, int userId)
        {
            var removeSettings = _context.Settings.Where(x => x.UserId == userId).ToList();
            _context.Settings.RemoveRange(removeSettings);
            await _context.SaveChangesAsync();
            await CreateSettingAsync(settings);
        }

        public Task<Setting?> GetDefaultPageByUserIdAsync(int userId)
        {
            return _context.Settings.FirstOrDefaultAsync(x => x.UserId == userId && x.Key == KeyConstans.DEFAULT_PAGE);
        }

        public async Task<User> ChangeUsernameByIdAsync(int id, string username)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == id);
            user.Name = username;
            await _context.SaveChangesAsync();
            return user;
        }

        public Task<User?> GetUserByIdAsync(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Setting>> GetUserSettingByIdAsync(int userId)
        {
            return _context.Settings.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
