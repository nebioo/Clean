using System;
using Domain.TodoAggregate;
using Infrastructure.Persistence;
using Repository;

namespace Infrastructure.RepositoryAggregate;

public class TodoRepository : GenericRepository<Todo>, ITodoRepository
{
    public TodoRepository(ApplicationDbContext context) : base(context)
    {
    }
}

