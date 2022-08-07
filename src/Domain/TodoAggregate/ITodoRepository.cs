using System;
using Clean.Repository;

namespace Domain.TodoAggregate;

public interface ITodoRepository : IGenericRepository<Todo>
{
}

