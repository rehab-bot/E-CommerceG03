using DomainLayer.Contracts;
using DomainLayer.Models;
using Presistance.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext  ): IUnitOfWork
    { private readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeof(TEntity).Name))
            {
                return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            }

            var repo = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[typeName] = repo;
            return repo;
        }  
            public async Task<int> SaveChangesAsync()
                {
                    return await _dbContext.SaveChangesAsync();
                }
    }
}
