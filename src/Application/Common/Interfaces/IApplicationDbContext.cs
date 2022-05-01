using Domain.TodoAggregate;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Todo> Todos { get; set; }
    }
}
