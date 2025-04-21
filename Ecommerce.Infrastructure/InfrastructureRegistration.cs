using Ecommerce.Core.Interfaces.Repositories.Contract;
using Ecommerce.Core.Interfaces.UnitOfWork;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Infrastructure.Repositories.Service;
using Ecommerce.Service.ImageService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Ecommerce.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EcommerceDatabase"));
            });


            // Add infrastructure services here
            services.AddScoped(typeof(IUnitOfWork), typeof(Ecommerce.Infrastructure.UnitOfWork.UnitOfWork));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddSingleton<IImageManagementService, ImageManagementService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            return services;
        }
    }
}
