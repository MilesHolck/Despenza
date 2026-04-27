using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T item);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id); //Eller Task DeleteAsync(T item);
    }
}
