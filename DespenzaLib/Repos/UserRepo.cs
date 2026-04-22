using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class UserRepo : IRepository<User>
    {
        public List<User> Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(User user)
        {
            Users.Add(user);
        }

        public void Delete(User user)
        {
            Users.Remove(user);
        }

        public User GetById(int id)
        {
            return Users.FirstOrDefault(u => u.UserId == id);
        }

        public List<User> GetAll()
        {
            return Users;
        }

        public User Update(User user)
        {
            return Users.FirstOrDefault(u => u.UserId == user.UserId);
        }
    }
}
