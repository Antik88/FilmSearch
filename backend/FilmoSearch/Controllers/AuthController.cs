using FilmoSearch.DTOs.UserDtos;
using FilmoSearch.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService actorService)
        {
            _authService = actorService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(CreateUserDto createUserDto) 
        {
            return await _authService.Register(createUserDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(CreateUserDto createUserDto)
        {
            return await _authService.Login(createUserDto);
        }

        [HttpPost("check")]
        public async Task<ActionResult<string>> Check()
        {
            var token = await _authService.CheckToken();
            return Ok(token);
        }

        [HttpGet("getName"), Authorize()]
        public ActionResult<string> GetName()
        {
            return Ok(_authService.GetName());
        }
    }
}
