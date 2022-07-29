using Microsoft.EntityFrameworkCore;
using System;
using Domain.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Mapper;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new TodoMapper().BaseMap(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.ChangeTracker.DetectChanges();
        var added = this.ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Added)
            .Select(t => t.Entity)
            .ToArray();

        foreach (var entity in added)
        {
            if (entity is not DomainBase track) continue;
            track.CreatedDate = DateTime.Now;
            track.SetIsActive(true);
        }

        var modified = this.ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Modified)
            .Select(t => t.Entity)
            .ToArray();

        foreach (var entity in modified)
        {
            if (entity is not DomainBase track) continue;
            track.ModifiedDate = DateTime.Now;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
