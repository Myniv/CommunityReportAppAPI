using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, string? includeProperties = null, Expression<Func<T, object>>? orderByExpression = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression, string? includeProperties = null, Expression<Func<T, object>>? orderByExpression = null);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        bool Remove(T entity);
        bool RemoveRange(IEnumerable<T> entities);
        Task<int> SaveAsync();
        Task<bool> UpdateAsync(T entity);
    }
}
