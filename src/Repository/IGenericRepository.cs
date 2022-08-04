namespace Repository;

public interface IGenericRepository<T> where T : RepositoryBase
{
    Task<List<T>> GetAllAsync(bool isActive = true);
    Task<T> GetByIdAsync(Guid id, bool isActive = true);
    Task SaveAsync(T entity);
    T Update(T entity);

}
