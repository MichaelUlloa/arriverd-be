using arriverd_be.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace arriverd_be.Data.Configuration;

public class ImageEntityTypeConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasOne(x => x.Excursion);
    }
}
