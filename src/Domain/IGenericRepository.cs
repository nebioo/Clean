using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain
{
    public interface IGenericRepository<TEntity> where TEntity : DomainBase
    {
        IQueryable<TEntity> Table();
        Task<TEntity> GetByIdAsync(Guid id, bool isActive = true);
        Task<List<TEntity>> AllAsync(bool isActive = true);
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> predicate, bool isActive = true);
        Task SaveAsync(TEntity entity);
        //to do update has to be async
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);

    }
}