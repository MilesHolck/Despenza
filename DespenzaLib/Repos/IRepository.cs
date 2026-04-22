using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public interface IRepository<T>
    {
       
        public void Add(T item);

        public List<T> GetAll();

        public T Update(T item); 

        public void Delete(T item);

        public T GetById(int id);
       

    }
}
