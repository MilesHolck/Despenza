using DespenzaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<User> _users;

        public AuthenticationService()
        {
            _users = new List<User>()
            {
                new Admin
                {
                    UserId = 1,
                    Username = "Camilla",
                    Email = "Camilla@Despenza.dk",
                    Password = "1234",
                    Role = "Admin"
                },

                new Baker
                {
                    UserId = 2,
                    Username = "Ida",
                    Email = "Ida@Despenza.dk",
                    Password = "1111",
                    Role = "User"
                }
            };
        }

           
        

        public bool IsAdmin(User user)
        {
            if (user.Role == "Admin") 
            { 
                return true;
                
            }
            return false;
        }

        public User? Login(string email, string password)
        {
            email = email?.Trim(); 

            User loggedInUser = _users.FirstOrDefault(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase) && u.Password == password);
            return loggedInUser;
        }


    }
}
