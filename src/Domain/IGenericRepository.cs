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
        Task Add(TEntity entity);
        Task<int> Count();
        Task<ICollection<TEntity>> Filter(Expression<Func<TEntity, bool>> match);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindByProperties(Expression<Func<TEntity, bool>> match, string includeProperties = "");
        Task<ICollection<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        IQueryable<TEntity> Table();
        TEntity Update(TEntity entity);

    }
}