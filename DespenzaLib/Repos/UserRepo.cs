using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class UserRepo : IUserRepo
    {
        public List<User> Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            Users.Remove(user);
        }

        public User GetUserById(int id)
        {
            return Users.FirstOrDefault(u => u.UserId == id);
        }

        public List<User> GetUsers()
        {
            return Users;
        }

        public User UpdateUser(User user)
        {
            return Users.FirstOrDefault(u => u.UserId == user.UserId);
        }
    }
}
