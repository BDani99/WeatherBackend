using Weather_App.Dtos;
using Weather_App.Models;

namespace Weather_App.Service
{
    public interface IAccountService
    {
        Task<User?> LoginAsync(LoginDto loginDto);
    }
}