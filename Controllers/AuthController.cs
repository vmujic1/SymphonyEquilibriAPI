using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SymphonyEquilibriAPI.Dtos.User;
using SymphonyEquilibriAPI.Models;
using SymphonyEquilibriAPI.Services.AuthService;

namespace SymphonyEquilibriAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authService.Register(
                new Models.User.User { Username = request.Username }, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authService.Login(request.Username, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}

