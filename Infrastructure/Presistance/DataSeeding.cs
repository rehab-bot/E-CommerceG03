using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Presistance
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {

        public async Task DataSeedAsync()
        {
            try
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.ProductBrands.Any())
                { 

                    var productBrandsData =File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeeding\brands.json");
                    var productBrands =  await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        await  _dbContext.ProductBrands.AddRangeAsync(productBrands);

                    }

                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypeData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeeding\types.json");
                    var productType =await  JsonSerializer.DeserializeAsync<List<ProductType>>(productTypeData);
                    if (productType is not null && productType.Any())
                    {
                      await  _dbContext.ProductTypes.AddRangeAsync(productType);

                    }

                }

                if (!_dbContext.Products.Any())
                {
                    var productsData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeeding\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                    if (products is not null && products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(products);

                    }

                }

               await  _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                //To Do
            }
        }

       
    }
}
