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

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }
        public async Task Update(T entity)
        {
            dbSet.Update(entity);
            await SaveAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task DeleteRangeAsync(T[] entity)
        {
            dbSet.RemoveRange(entity);
            await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
