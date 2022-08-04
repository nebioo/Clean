using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : RepositoryBase
    {
        public readonly DbSet<T> entities;

        protected GenericRepository(DbContext context)
        {
            entities = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(bool isActive = true)
        {
            return await entities.Where(s => s.IsActive == isActive).AsQueryable().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, bool isActive = true)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id && s.IsActive == isActive);
        }

        public async Task SaveAsync(T entity)
        {
            await entities.AddAsync(entity);
        }
        public T Update(T entity)
        {
            var entityEntry = entities.Update(entity);
            return entityEntry.Entity;
        }

    }
}