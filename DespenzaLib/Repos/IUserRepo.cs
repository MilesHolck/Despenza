using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public interface IUserRepo
    {
        public List<User> Users { get; set; }
        public void AddUser(User user); 

        public User UpdateUser(User user); 
        public void DeleteUser(User user);
        public User GetUserById(int id);
        public List<User> GetUsers();

    }
}
