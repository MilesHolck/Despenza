using DespenzaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Services
{
    public interface IAuthenticationService
    {
        User? Login(string username, string password);
        bool IsAdmin(User user);
    }
}
