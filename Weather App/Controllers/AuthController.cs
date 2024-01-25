using Academy_2023.Services;
using Microsoft.AspNetCore.Mvc;
using Secret_Sharing_Platform.Dto;
using Weather_App.Dtos;
using Weather_App.Helpers;
using Weather_App.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weather_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AuthController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Post([FromBody] LoginDto loginDto)
        {
            var user = await _accountService.LoginAsync(loginDto);
            if (user == null)
            {
                return Unauthorized(new { error = ErrorConstans.WRONG_EMAIL_OR_PASSWORD } );
            }
            var tokenDto = _tokenService.CreateToken(user);
            return Ok(tokenDto);
        }
    }
}
