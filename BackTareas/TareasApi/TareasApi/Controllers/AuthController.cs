using TareasApi.Application.DTOs;
using TareasApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace TareasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "1234")
            {
                var token = _jwtTokenService.GenerateToken(request.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized(new ErrorDto { Codigo = 401, Mensaje = "Credenciales inválidas" });
        }

    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
