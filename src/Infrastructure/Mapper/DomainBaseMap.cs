using Clean.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapper;

public abstract class DomainBaseMap<T> where T : RepositoryBase
{
    protected abstract void Map(EntityTypeBuilder<T> eb);

    public void BaseMap(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<T>(bi =>
        {
            bi.Property(b => b.CreatedDate).HasColumnType("timestamp").HasColumnOrder(200);
            bi.Property(b => b.ModifiedDate).HasColumnType("timestamp").HasColumnOrder(201);
            bi.Property(b => b.IsActive).HasColumnType("bit(1)").HasColumnName("isActive").HasColumnOrder(202).HasConversion(ConverterProvider.GetBoolToBitArrayConverter());
            bi.HasKey(b => b.Id);
            Map(bi);
        });
    }
}