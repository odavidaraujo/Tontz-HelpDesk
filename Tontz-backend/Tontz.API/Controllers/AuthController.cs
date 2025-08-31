using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tontz.Infrastructure.Data;
using Tontz.API.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Tontz.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly TontzDbContext _context;

        public AuthController(TontzDbContext context) {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) {
            // Busca usuário no banco
            var user = await _context.TB_FUNCIONARIO
                .FirstOrDefaultAsync(u => u.NOME == request.Username);

            if (user == null)
                return Unauthorized("Usuário não encontrado");

            // Compara diretamente a senha (texto puro)
            if (user.SENHA != request.Password)
                return Unauthorized("Senha incorreta");

            // Gera token
            var token = GenerateJwtToken(user.NOME, user.ESTADO);
            return Ok(new { token });
        }


        private string GenerateJwtToken(string username, string ESTADO) {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKeyProvider.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, ESTADO)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public static class JwtKeyProvider {
        public static readonly string Key;

        static JwtKeyProvider() {
            var keyBytes = RandomNumberGenerator.GetBytes(32); // 32 bytes = 256 bits
            Key = Convert.ToBase64String(keyBytes);
        }
    }
}
