using Clean.Repository;
using Domain.TodoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapper;

public class TodoMapper : DomainBaseMap<Todo>
{
    protected override void Map(EntityTypeBuilder<Todo> eb)
    {
        eb.Property(b=>b.Name).HasColumnType("varchar(50)");
        eb.Property(b=>b.Description).HasColumnType("varchar(500)");
        eb.Property(b=>b.IsCompleted).HasColumnType("bit(1)").HasColumnName("isActive").HasConversion(ConverterProvider.GetBoolToBitArrayConverter());

        eb.ToTable("Todo");
    }
}

