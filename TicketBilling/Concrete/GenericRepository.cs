using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketBilling.Abstract;
using TicketBilling.Models;

namespace TicketBilling.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly db_a483f5_usertestContext _context;

        public GenericRepository(db_a483f5_usertestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAsQuerable()
            =>  _context.Set<TEntity>();

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().AsNoTracking().ToListAsync();


        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
