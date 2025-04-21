using AutoMapper;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces.Repositories.Contract;
using Ecommerce.Core.Interfaces.UnitOfWork;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Service.ImageService;
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
        private IProductRepository _productRepository;

        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;

        ///public ICategoryRepository CategoryRepository { get; }
        ///public IProductRepository ProductRepository { get; }
        ///public IPictureRepository PictureRepository { get; }

        //private Dictionary<string, GenericRepository<object>> _repositoryStore;
        private Dictionary<string, object > _repositoryStore;


        public UnitOfWork(StoreContext storeContext, IMapper mapper, IImageManagementService imageManagementService)
        {
            _storeContext = storeContext;
            _repositoryStore = new Dictionary<string, object>();
            _mapper = mapper;
            _imageManagementService = imageManagementService;
            ///CategoryRepository = new CategoryRepository(_storeContext);
            ///ProductRepository = new ProductRepository(_storeContext);
            ///PictureRepository = new PictureRepository(_storeContext);
        }


        //public IProductRepository ProductRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_storeContext, _mapper,_imageManagementService);
                }
                return _productRepository;
            }
        }


        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : class
        {
            var key = typeof(TEntity).Name;

            if(!_repositoryStore.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_storeContext)  ;
                _repositoryStore.Add(key, repository);
            }
            return (IGenericRepository<TEntity>)_repositoryStore[key];
        }

        public async Task<int> SaveChangesAsync()
            => await _storeContext.SaveChangesAsync();


        public async ValueTask DisposeAsync()
            => await _storeContext.DisposeAsync();

    }
}
