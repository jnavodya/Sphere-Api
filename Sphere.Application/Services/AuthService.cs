using Microsoft.IdentityModel.Tokens;
using Sphere.Application.Interfaces;
using Sphere.Application.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sphere.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly JWTSettings _jwtSettings;

        // In-memory user list
        private static List<UserModel> _users = new List<UserModel>
        {
            new UserModel { Username = "admin", Password = "password" },
            new UserModel { Username = "testuser", Password = "123456" }
        };

        public AuthService(JWTSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        public string Authenticate(UserModel model)
        {
            var user = _users.FirstOrDefault(u => u.Username == model.Username);

            if (user != null)
            {
                // User exists: check password
                if (user.Password != model.Password)
                {
                    return "Unauthorized"; // Invalid password
                }
            }
            else
            {
                // New user: register
                _users.Add(new UserModel { Username = model.Username, Password = model.Password });
            }

            // Generate and return JWT token
            return GenerateToken(model.Username);
        }

        public string GenerateToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
