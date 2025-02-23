using TareasApi.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TareasApi.Infrastructure.Auth
{
    public class JwtTokenService : IJwtTokenService
    {

        private readonly IConfiguration _configuration;
        private readonly ILoggerService _logger;

        public JwtTokenService(IConfiguration configuration, ILoggerService logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string GenerateToken(string username)
        {

            try
            {
                var secretKey = _configuration["JwtSettings:Secret"];
                if (string.IsNullOrEmpty(secretKey))
                    throw new Exception("La clave secreta para JWT no está configurada.");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
        new Claim(ClaimTypes.Name, username)
    };

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                // Puedes loguear el error si tienes un servicio de logging
                _logger.Log($"Error al generar el token JWT: {ex.Message}");

                // Puedes lanzar la excepción o devolver un mensaje personalizado
                throw new Exception("Ocurrió un error al generar el token JWT.");
            }


        }

    }
}
