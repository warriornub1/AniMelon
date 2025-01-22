using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OneLearn.Application.Common;
using OneLearn.Domain.Common;
using OneLearn.Infrastructure.Common.DBContexts;
using System.Linq.Expressions;

namespace OneLearn.Infrastructure.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> condition)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(condition);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }
        public async Task UpdateAsAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateRangeAsAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => _dbContext.Set<T>().UpdateRange(entities));
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => _dbContext.Set<T>().RemoveRange(entities));
        }
        public async Task<IEnumerable<T>> GetQueryPaged(int page, int pageSize, Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>()
                .Where(predicate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> CountQueryPaged(int page, int pageSize, Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>()
                .Where(predicate)
                .CountAsync();
        }
        public async Task<IEnumerable<T>> FindMultiple(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().Where(expression).ToListAsync();
        }
        public async Task <T> Find(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().Where(expression).FirstOrDefaultAsync();
        }
        public async Task<Dictionary<int, T>> GetDictionaryAsync(Expression<Func<T, bool>> condition)
        {
            return await _dbContext.Set<T>().Where(condition).ToDictionaryAsync(x => x.id, x => x);
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await _dbContext.Set<T>().Where(x => x.id == id).FirstOrDefaultAsync();
            return entity != null;
        }

    }
}
