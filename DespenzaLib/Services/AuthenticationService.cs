using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private List<User> _users => throw new NotImplementedException();

        public bool IsAdmin(User user)
        {
            throw new NotImplementedException();
        }

        public User? Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
