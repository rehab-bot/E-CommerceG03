using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductCountSpecification :BaseSpecification<Product ,int>
    {
        public ProductCountSpecification(ProductQueryParams productQueryParams)
          :  base(P=>(!productQueryParams.brandId.HasValue || P.BrandId== productQueryParams.brandId)
            && (!productQueryParams.typeId.HasValue || P.TypeId == productQueryParams.typeId)
            &&(string.IsNullOrWhiteSpace(productQueryParams.searchValue) || P.Name.ToLower().Contains(productQueryParams.searchValue.ToLower())))
        {

        }
    }
}
