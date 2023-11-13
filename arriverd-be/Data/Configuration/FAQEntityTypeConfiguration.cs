using arriverd_be.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace arriverd_be.Data.Configuration;

public class FAQEntityTypeConfiguration : IEntityTypeConfiguration<FAQ>
{
    public void Configure(EntityTypeBuilder<FAQ> builder)
    {
        builder
            .Property(p => p.Question)
            .HasColumnType("varchar(80)");
    }
}
