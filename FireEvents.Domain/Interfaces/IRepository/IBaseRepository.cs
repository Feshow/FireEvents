using System.Linq.Expressions;

namespace FireEvents.Domain.Interfaces.IRepository
{
    public interface IBaseRepository<T> where T : class
    {

        Task<bool> AddAsync(T model);
        Task<bool> Update(T model);
        Task<bool> DeleteAsync(T model);
        Task<bool> DeleteRangeAsync(T[] model);
        Task<bool> SaveAsync();
    }
}
