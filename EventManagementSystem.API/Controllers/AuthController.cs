using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Data.Context;
using EventManagementSystem.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManagementSystem.API.Controllers
{
    // This controller handles user registration and login operations.
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        
        public AuthController(AppDbContext context, IConfiguration configuration) // Constructor to inject the database context and configuration settings.

        {
            _context = context;
            _configuration = configuration;
        }

        // Endpoint for user registration
        // Checks if the email is already registered before saving the new user
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (existingUser != null)
                return BadRequest("Bu email adresi zaten kayıtlı.");

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Kayıt başarılı.");
        }

        // Endpoint for user login
        // Verifies user credentials and returns a JWT token if valid
        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == loginUser.Email && x.Password == loginUser.Password);
            if (user == null)
                return Unauthorized("Hatalı giriş.");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user) // Private helper method to generate JWT token for authenticated users.

        {
            var claims = new[] // Claims to include in the token (user ID and role)

            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var jwtKey = _configuration["Jwt:Key"]; // Retrieve the secret key from configuration.

            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT Key bulunamadı! Lütfen appsettings.json dosyasını kontrol et.");
            
            // Create the signing credentials using the secret key.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create and return the JWT token.
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
