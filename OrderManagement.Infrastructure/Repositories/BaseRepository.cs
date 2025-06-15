using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        

        public virtual void Delete(T entity) => _dbSet.Remove(entity);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);


        public virtual void Update(T entity) => _dbSet.Update(entity);
    }
}
