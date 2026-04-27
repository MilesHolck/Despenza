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

        public async Task AddAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync(); 
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T item)
        {
            _dbSet.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item != null)
            {
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
