using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
     class ProductWithBrandAndTypeSpecification:BaseSpecification<Product, int>
    {    //Get All
        public ProductWithBrandAndTypeSpecification(ProductQueryParams productQueryParams)
            : base(P=>(!productQueryParams.brandId.HasValue || P.BrandId== productQueryParams. brandId)
            && (!productQueryParams.typeId.HasValue || P.TypeId == productQueryParams.typeId)
            &&(string.IsNullOrWhiteSpace(productQueryParams.searchValue) || P.Name.ToLower().Contains(productQueryParams.searchValue.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch(productQueryParams.sortingOption)
            {   case ProductSortingOption.NameAsc:
                    AddOrderBy(n => n.Name);
                    break;
                case ProductSortingOption.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOption.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOption.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                    default:
                    break;
            }
            ApplyPagination( productQueryParams.PageSize, productQueryParams.PageIndex);
        }  
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
