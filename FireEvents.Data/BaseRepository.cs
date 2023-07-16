using FireEvents.Data.Contexts;
using FireEvents.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FireEvents.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return await SaveAsync();
        }
        public async Task<bool> Update(T entity)
        {
            dbSet.Update(entity);
            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteRangeAsync(T[] entity)
        {
            dbSet.RemoveRange(entity);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
