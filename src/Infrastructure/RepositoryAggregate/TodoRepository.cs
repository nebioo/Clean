using System;
using Domain.TodoAggregate;
using Infrastructure.Persistence;

namespace Infrastructure.RepositoryAggregate;

public class TodoRepository : GenericRepository<Todo>, ITodoRepository
{
    public TodoRepository(ApplicationDbContext context) : base(context)
    {
    }
}

