using arriverd_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace arriverd_be.Data;

public class ArriveDbContext : DbContext
{
    public ArriveDbContext(DbContextOptions<ArriveDbContext> options) : base(options)
    {
    }

    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethods");
    }
}
