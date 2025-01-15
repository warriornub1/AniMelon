using OneLearn.Domain.Common;
using System.Linq.Expressions;

namespace OneLearn.Application.Common
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> condition);
        Task CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        Task UpdateAsAsync(T entity);
        Task UpdateRangeAsAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetQueryPaged(int page, int pageSize, Expression<Func<T, bool>> predicate);
        Task<int> CountQueryPaged(int page, int pageSize, Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindMultiple(Expression<Func<T, bool>> expression);
        Task<T> Find(Expression<Func<T, bool>> expression);
        Task<Dictionary<int, T>> GetDictionaryAsync(Expression<Func<T, bool>> condition);
        Task<bool> Exists(int id);
    }
}
