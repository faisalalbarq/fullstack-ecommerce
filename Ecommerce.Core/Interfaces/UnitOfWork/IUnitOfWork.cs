using Ecommerce.Core.Interfaces.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : class;

        IProductRepository ProductRepository { get; }
        //public ICategoryRepository CategoryRepository { get;}
        //public IProductRepository ProductRepository { get; }
        //public IPictureRepository PictureRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
