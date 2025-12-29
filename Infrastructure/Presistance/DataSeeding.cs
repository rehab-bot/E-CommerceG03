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

        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandsData = File.ReadAllText(@"..\Infrastructure\Presistance\Data\DataSeeding\brands.json");
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(productBrands);

                    }

                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypeData = File.ReadAllText(@"..\Infrastructure\Presistance\Data\DataSeeding\types.json");
                    var productType = JsonSerializer.Deserialize<List<ProductType>>(productTypeData);
                    if (productType is not null && productType.Any())
                    {
                        _dbContext.ProductTypes.AddRange(productType);

                    }

                }

                if (!_dbContext.Products.Any())
                {
                    var productsData = File.ReadAllText(@"..\Infrastructure\Presistance\Data\DataSeeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products is not null && products.Any())
                    {
                        _dbContext.Products.AddRange(products);

                    }

                }

                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                //To Do
            }
        }
    }
}
