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
    public class GenericRepository<T> : IGenericRepository<T> where T : DomainBase
    {
        public readonly DbSet<T> entities;

        protected GenericRepository(DbContext context)
        {
            this.entities = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(bool isActive = true)
        {
            return await this.entities.Where(s => s.IsActive == isActive).AsQueryable().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, bool isActive = true)
        {
            return await this.entities.SingleOrDefaultAsync(s => s.Id == id && s.IsActive == isActive);
        }

        public async Task SaveAsync(T entity)
        {
            await this.entities.AddAsync(entity);
        }
        public T Update(T entity)
        {
            var entityEntry = this.entities.Update(entity);
            return entityEntry.Entity;
        }

    }
}