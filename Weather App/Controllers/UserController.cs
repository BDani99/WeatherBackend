using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Secret_Sharing_Platform.Dtos;
using System.ComponentModel.DataAnnotations;
using Weather_App.Dtos;
using Weather_App.Helpers;
using Weather_App.Models;
using Weather_App.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weather_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IValidator<RegisterDto> _validator;
        private readonly IUserService _userService;

        public UserController(IValidator<RegisterDto> validator, IUserService userService)
        {
            _validator = validator;
            _userService = userService;
        }


        //POST api/<UserController>
        [HttpPost("registration")]
        public async Task<ActionResult> PostAsync([FromBody] RegisterDto registerDto)
        {
            var result = await _validator.ValidateAsync(registerDto);

            if (!result.IsValid)
            {
                return BadRequest(new { errors = result.Errors.First().ErrorMessage });
            }

            registerDto.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            
            await _userService.CreateUserAsync(registerDto);
            
            return NoContent();
        }
        [Authorize]
        [HttpPut("update-setting")]
        public async Task<ActionResult> PutAsync([FromBody] SettingDto settingDto)
        {
            var userId = _userService.GetUserId(User);
            if (userId == null) return BadRequest(new { error = 
                ErrorConstans.CAN_NOT_FIND_USER_ID });
            await _userService.UpdateSettingsAsync(settingDto, userId.Value);
            return NoContent();
        }

        [Authorize]
        [HttpGet("defaultpage")]
        public async Task<ActionResult<DefaultPageDto>> DefaultPageByUserIdAsync()
        {
            var userId = _userService.GetUserId(User);
            if (userId == null)
            {
                return BadRequest(new { error = ErrorConstans.CAN_NOT_FIND_USER_ID });
            }

            var setting =await  _userService.GetDefaultPageByUserIdAsync(userId.Value);
            if (setting == null)
            {
                return BadRequest(new { error = ErrorConstans.CAN_NOT_FIND_THIS_SETTING });
            }

            return Ok(new DefaultPageDto() { Value = setting.Value });
        }

        [Authorize]
        [HttpPut("update-user")]
        public async Task<ActionResult> PutUserAsync([FromBody] UserDto userDto)
        {
            var userId = _userService.GetUserId(User);
            if (userId == null) return BadRequest(new
            {
                error =
                ErrorConstans.CAN_NOT_FIND_USER_ID
            });
            await _userService.UpdateUserAsync(userDto, userId.Value);
            return NoContent();
        }
        
        [Authorize]
        [HttpPut("update-user-mobile")]
        public async Task<ActionResult> PutUserMobileAsync([FromBody] UserMobileDto userMobileDto)
        {
            var userId = _userService.GetUserId(User);
            if (userId == null) return BadRequest(new
            {
                error =
                ErrorConstans.CAN_NOT_FIND_USER_ID
            });
            await _userService.UpdateUserMobileAsync(userMobileDto, userId.Value);
            return NoContent();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetMeAsync()
        {
            var id = _userService.GetUserId(User);
            if (id == null) { return BadRequest(ErrorConstans.CAN_NOT_FIND_USER_ID); }
            var userDto = await _userService.GetUsernameAndSettingsByIdAsync(id.Value);
            if (userDto == null)
            {
                return BadRequest(ErrorConstans.CAN_NOT_FIND_USER);
            }
            return Ok(userDto);
        }
    }
}
