using Domain.Entities;
using Domain.FileAggregate;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<File> Files { get; set; }
    }
}
