using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper) : IServiceManager
    { private readonly Lazy<IProductService> _lazyproductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork ,_mapper));
        public IProductService ProductService => _lazyproductService.Value;
    }
}
