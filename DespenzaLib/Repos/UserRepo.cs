using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class UserRepo : IRepository<User>
    {
        public List<User> Collection { get; set; }

        public void Add(User user)
        {
            Collection.Add(user);
        }

        public void Delete(User user)
        {
            Collection.Remove(user);
        }

        public User GetById(int id)
        {
            return Collection.FirstOrDefault(u => u.UserId == id);
        }

        public List<User> GetAll()
        {
            return Collection;
        }

        public User Update(User user)
        {
            return Collection.FirstOrDefault(u => u.UserId == user.UserId);
        }
    }
}
