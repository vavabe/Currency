using CurrencyServices.UserApp.Application.Exceptions;
using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyServices.UserApp.RestApi.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var result = await _userService.Register(registerDto);
            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _userService.Login(loginDto);
                if (token is not null)
                    return Ok(new TokenDto { Token = token });
                else
                    return BadRequest();
            }
            catch (WrongCredentialsException)
            {
                return BadRequest();
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutUser([FromBody] LogoutDto logoutDto, CancellationToken cancellationToken)
        {
            var result = await _userService.Logout(logoutDto);
            if (result)
                return Ok();
            else
                return BadRequest();
        }
    }
}
