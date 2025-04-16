using Ecommerce.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "DataSeeding");

            if (!context.Categories.Any())
            {
                var categoryPath = Path.Combine(basePath, "Category.json");
                var categoryData = File.ReadAllText(categoryPath);
                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                if (categories is not null)
                    await context.Categories.AddRangeAsync(categories);
            }

            if (!context.Products.Any())
            {
                var productPath = Path.Combine(basePath, "Product.json");
                var productData = File.ReadAllText(productPath);
                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products is not null)
                    await context.Products.AddRangeAsync(products);
            }

            if (!context.pictures.Any())
            {
                var picturePath = Path.Combine(basePath, "Picture.json");
                var pictureData = File.ReadAllText(picturePath);
                var pictures = JsonSerializer.Deserialize<List<Picture>>(pictureData);

                if (pictures is not null)
                    await context.pictures.AddRangeAsync(pictures);
            }

            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
