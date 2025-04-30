using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LayeredArchitecture.Data.Repositories;
using LayeredArchitecture.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LayeredArchitecture.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly AuthRepo _repo;

        public JwtService(IConfiguration configuration, AuthRepo repo)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Retrieve JWT settings
            var jwtSettings = _configuration.GetSection("Jwt");
            if (string.IsNullOrEmpty(jwtSettings["Key"]) ||
                string.IsNullOrEmpty(jwtSettings["Issuer"]) ||
                string.IsNullOrEmpty(jwtSettings["Audience"]) ||
                string.IsNullOrEmpty(jwtSettings["ExpiresInMinutes"]))
            {
                throw new InvalidOperationException("JWT settings are not properly configured in appsettings.json.");
            }

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Add roles to claims
            var roles = await _repo.GetUserRole(user);
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            // Generate JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
 
}