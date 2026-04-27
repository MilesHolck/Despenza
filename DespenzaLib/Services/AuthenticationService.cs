using DespenzaLib.Models;
using DespenzaLib.Data; // Henter din AppDbContext
using System;
using System.Linq;

namespace DespenzaLib.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly AppDbContext _context;

        // private readonly List<User> _users;

        // 2. Vi injicerer Databasen via constructoren
        public AuthenticationService(AppDbContext context) //skift AppDB... ud med repo for at bruge repo :D 
        {
            _context = context; //skift AppDB... ud med repo for at bruge repo :D 

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

        public User? LogIn(string email, string password)
        {
            email = email?.Trim();

            // Vi bruger bare .ToLower() for at være 100% sikre på at 
            // store/små bogstaver ikke driller, og et helt normalt == tegn.
            // Dette kan Entity Framework nemt oversætte til SQL!
            User loggedInUser = _context.Users.FirstOrDefault(u =>
                u.Email.ToLower() == email.ToLower() &&
                u.Password == password);

            return loggedInUser;
        }

    }
}