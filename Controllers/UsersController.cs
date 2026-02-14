using MarShield.API.Models;
using MarShield.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarShield.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        // Importing services via Dependency Injection
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        //Get all Users
        // GET: api/users
        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await _userService.GetAsync();
        }

        // POST: api/users (REGISTER)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            // Check trùng username (nếu cần kỹ hơn)
            await _userService.CreateAsync(newUser);
            return Ok(new { message = "Đăng ký thành công!", userId = newUser.Id });
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.LoginAsync(request.Username, request.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Sai tài khoản hoặc mật khẩu!" });
            }

            // Return user (Unity will save ID and money)
            // Remove password hash before sending back
            user.PasswordHash = "";

            return Ok(user);
        }
    }
}
