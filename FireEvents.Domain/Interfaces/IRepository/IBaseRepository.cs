using System.Linq.Expressions;

namespace FireEvents.Domain.Interfaces.IRepository
{
    public interface IBaseRepository<T> where T : class
    {

        Task AddAsync(T model);
        Task Update(T model);
        Task DeleteAsync(T model);
        Task DeleteRangeAsync(T[] model);
        Task<bool> SaveAsync();
    }
}
