using arriverd_be.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace arriverd_be.Data.Configuration;

public class ScheduleEntityTypeConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder
            .Property(p => p.Itinerary)
            .HasColumnType("varchar(max)");
    }
}
