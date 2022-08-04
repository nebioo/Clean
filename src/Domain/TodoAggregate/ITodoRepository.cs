using System;
using Repository;

namespace Domain.TodoAggregate;

public interface ITodoRepository : IGenericRepository<Todo>
{
}

