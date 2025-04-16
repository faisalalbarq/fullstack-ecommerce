using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces.Repositories.Contract;
using Ecommerce.Core.Interfaces.UnitOfWork;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;

        ///public ICategoryRepository CategoryRepository { get; }
        ///public IProductRepository ProductRepository { get; }
        ///public IPictureRepository PictureRepository { get; }

        private Dictionary<string, GenericRepository<object>> _repositoryStore;
        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repositoryStore = new Dictionary<string, GenericRepository<object>>();
            
            ///CategoryRepository = new CategoryRepository(_storeContext);
            ///ProductRepository = new ProductRepository(_storeContext);
            ///PictureRepository = new PictureRepository(_storeContext);
        }
        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : class
        {
            var key = typeof(TEntity).Name;

            if(!_repositoryStore.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_storeContext) as GenericRepository<object>;
                _repositoryStore.Add(key, repository);
            }
            return _repositoryStore[key] as IGenericRepository<TEntity>;
        }

        public async Task<int> SaveChangesAsync()
            => await _storeContext.SaveChangesAsync();


        public async ValueTask DisposeAsync()
            => await _storeContext.DisposeAsync();

    }
}
