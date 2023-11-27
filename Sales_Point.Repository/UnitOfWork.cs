using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales_Point.Core;
using Sales_Point.Core.Entities;
using Sales_Point.Core.Repository;
using Sales_Point.Repository.Data;

namespace Sales_Point.Repository
{
    public class UnitOfWork: IUnitOfWork
    {

        private readonly StoreContext _dbContext;

        private Hashtable _repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {

            var type = typeof(TEntity).Name; 
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);

                _repositories.Add(type, repository);

            }

            return _repositories[type] as IGenericRepository<TEntity>;
        }


        public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();


        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();

    }
}
