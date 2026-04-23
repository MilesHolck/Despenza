using DespenzaLib.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class EFRepository<T> : IRepository<T> where T : class
    {

        private readonly AppDbContext _context; 
        private readonly DbSet<T> _dbSet; 

        public EFRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>(); 
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges(); 
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges(); 
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList(); 
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id); 
        }

        public T Update(T item)
        {
            _dbSet.Update(item);
            _context.SaveChanges();
            return item; 
        }
    }
}
