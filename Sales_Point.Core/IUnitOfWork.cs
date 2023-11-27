using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales_Point.Core.Entities;
using Sales_Point.Core.Repository;

namespace Sales_Point.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> CompleteAsync();
    }
}
