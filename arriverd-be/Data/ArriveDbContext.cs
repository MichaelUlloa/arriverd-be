using arriverd_be.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace arriverd_be.Data;

public class ArriveDbContext : IdentityDbContext
{
    public ArriveDbContext(DbContextOptions<ArriveDbContext> options) : base(options)
    {
    }

    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Excursion> Excursions { get; set; }
}
