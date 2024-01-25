using Secret_Sharing_Platform.Dto;
using Weather_App.Models;

namespace Academy_2023.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);
    }
}