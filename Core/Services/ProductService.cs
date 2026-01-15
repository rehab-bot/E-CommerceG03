using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Services.Specifications;
using Shared;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
        var brandDto =_mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandDto;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams productQueryParams)
        {  var repo =_unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecification(productQueryParams);
            var products = await repo.GetAllAsync(specification);
            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var productCount =productsDto.Count();
            var CountSpec = new ProductCountSpecification(productQueryParams);
            var totalCount = await repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(productCount, productQueryParams.PageIndex, totalCount, productsDto);
                   
        }

        public  async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
           var repo =_unitOfWork.GetRepository<ProductType, int>();
            var types =   await repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
           
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
           var repo =_unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecification(id);
          var product = await  repo.GetByIdAsync(specification);
            if(product == null)
                
                return null;
            

            return _mapper.Map<Product, ProductDto>(product);
        }
    }
}
