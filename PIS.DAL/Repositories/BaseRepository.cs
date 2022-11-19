using Microsoft.EntityFrameworkCore;
using PIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIS.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _currentSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _currentSet = _context.Set<T>();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _currentSet.FindAsync(id);

            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _currentSet.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
            => await _currentSet.ToListAsync();

        public async Task<T> GetAsync(int id)
            => await _currentSet.FindAsync(id);

        public async Task<int> InsertAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await _currentSet.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public async Task<int> UpdateAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _currentSet.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
