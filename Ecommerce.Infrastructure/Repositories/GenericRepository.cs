using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces.Repositories.Contract;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StoreContext _storeContext;
        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _storeContext.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _storeContext.Set<T>().AsQueryable();

            foreach (var items in includes)
            {
                query = query.Include(items);
            }
            return await query.ToListAsync();
        }



        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _storeContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _storeContext.Set<T>();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            var entity = await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
            return entity;
        }


        public async Task AddAsync(T entity)
        {
            await _storeContext.Set<T>().AddAsync(entity);
            await _storeContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _storeContext.Set<T>().FindAsync(id);
            _storeContext.Set<T>().Remove(entity);
            await _storeContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _storeContext.Entry(entity).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();
        }
    }
}
