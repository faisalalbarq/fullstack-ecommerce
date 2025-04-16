using Ecommerce.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecommerce.Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) 
            : base(options) 
            // I will pass the options to the base class constructor because
            // EF Core Knows how to Configure my DbContext
        {
            // I will take an instance of the DbContextOptions class because
            // my DbContext needs to Know how to connect to the database 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This method will be called when the model is being created

            // I will not call the base method here because the DbContext class does not have DbSet properties
            //base.OnModelCreating(modelBuilder);



            #region ApplyConfigurationsFromAssembly method
            // I will use the ApplyConfigurationsFromAssembly method:
            // this method will scan all the classes that implement the IEntityTypeConfiguration interface 
            // and apply the configurations to the model 
            #endregion

            // I will create the configuration classes for the domain models of the product module
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        
        
        }


        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Picture> pictures { get; set; }
    }
}
