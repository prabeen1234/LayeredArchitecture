using LayeredArchitecture.Data.Repositories;
using LayeredArchitecture.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArchitecture.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepo _authRepo;
        private readonly IJwtService _service;
        public AuthService(AuthRepo authRepo, IJwtService service)
        {
            _authRepo = authRepo;
            _service = service;
        }

        public Task<bool> AuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterAsync(string email, string password)
        {
            var existingUser = await _authRepo.GetByEmailAsync(email);
            if (existingUser != null)
                return "User already exists";

            var user = new ApplicationUser { Email = email, UserName = email };
            var result = await _authRepo.CreateAsync(user, password);

            if (result.Succeeded)
                return "User registered successfully";

            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return $"Registration failed: {errors}";
        }
        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _authRepo.GetByEmailAsync(email);
            if (user == null  ) // Replace with Identity password check
                return null;

            return _service.GenerateToken(user);

        }
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // lowercase hex
                }

                return builder.ToString();
            }
        }
    }
}
