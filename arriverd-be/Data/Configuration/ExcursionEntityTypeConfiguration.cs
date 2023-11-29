using arriverd_be.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace arriverd_be.Data.Configuration;

public class ExcursionEntityTypeConfiguration : IEntityTypeConfiguration<Excursion>
{
    public void Configure(EntityTypeBuilder<Excursion> builder)
    {
        builder
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
    }
}
