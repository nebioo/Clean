using Application.Common.Interfaces;
using Domain.Entities;
using Domain.FileAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }
    }
}
