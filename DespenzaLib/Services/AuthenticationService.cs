using DespenzaLib.Models;
using DespenzaLib.Data; // Henter din AppDbContext 
using System;
using System.Linq;
using DespenzaLib.Repos;
using Microsoft.EntityFrameworkCore;

namespace DespenzaLib.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IRepository<User> _userRepository;

        // private readonly List<User> _users;

        // 2. Vi injicerer Databasen via constructoren
        public AuthenticationService(IRepository<User> userRepository) //skift mellem MemoryRepo og EFRepo i Program.cs
        {
            _userRepository = userRepository;

            /* --- GAMMEL LISTE ---
            _users = new List<User>()
            {
                new Admin
                {
                    UserId = 1,
                    Name = "Camilla",
                    Email = "Camilla@Despenza.dk",
                    Password = "1234",
                    Role = "Admin"
                },
                new Baker
                {
                    UserId = 2,
                    Name = "Ida",
                    Email = "Ida@Despenza.dk",
                    Password = "1111",
                    Role = "User"
                }
            };
            ----------------------- */
        }

        public bool IsAdmin(User user)
        {
            if (user.Role == "Admin")
            {
                return true;
            }
            return false;
        }

        public async Task<User?> LogInAsync(string email, string password)
        {
            email = email?.Trim().ToLower();

            var users = await _userRepository.GetAllAsync();

            return users.FirstOrDefault(u =>
                u.Email.ToLower() == email &&
                u.Password == password);
        }

    }
}