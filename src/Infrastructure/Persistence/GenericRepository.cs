using Domain;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : DomainBase
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<TEntity> Table()
        {
            return context.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<ICollection<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> match)
        {
            return await context.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public virtual async Task<TEntity> FindByProperties(Expression<Func<TEntity, bool>> match, string includeProperties = "")
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (match != null)
                query = query.Where(match);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<ICollection<TEntity>> Filter(Expression<Func<TEntity, bool>> match)
        {
            return await context.Set<TEntity>().Where(match).ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            var entityEntry = context.Set<TEntity>().Update(entity);
            return entityEntry.Entity;
        }

        public virtual async Task<int> Count()
        {
            return await context.Set<TEntity>().CountAsync();
        }
    }
}