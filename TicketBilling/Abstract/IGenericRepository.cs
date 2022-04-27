using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TicketBilling.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        Task AddAsync(TEntity entity);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        IQueryable<TEntity> GetAsQuerable();
    }
}
