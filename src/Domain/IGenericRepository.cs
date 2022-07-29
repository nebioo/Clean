using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain;

public interface IGenericRepository<T> where T : DomainBase
{
    Task<List<T>> GetAllAsync(bool isActive = true);
    Task<T> GetByIdAsync(Guid id, bool isActive = true);
    Task SaveAsync(T entity);
    T Update(T entity);

}
