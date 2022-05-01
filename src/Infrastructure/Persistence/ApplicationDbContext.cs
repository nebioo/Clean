using Domain.TodoAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using Domain.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base (options)
        {

        }

        #region Domains
        public DbSet<TodoItem> Todos { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }

        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }

        private void PreInsertListener()
        {
            foreach (var entity in ChangeTracker.Entries<DomainBase>().Where(x => x.State == EntityState.Added).ToList())
            {
                entity.Entity.CreatedDate = DateTime.Now;
            }
        }

        private void UpdateListener()
        {
            foreach (var entity in ChangeTracker.Entries<DomainBase>().Where(x => x.State == EntityState.Modified).ToList())
            {
                entity.Entity.ModifiedDate = DateTime.Now;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            PreInsertListener();
            UpdateListener();
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
