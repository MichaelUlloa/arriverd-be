using arriverd_be.Entities;
using arriverd_be.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace arriverd_be.Data;

public class ArriveDbContext : IdentityDbContext
{
    public ArriveDbContext(DbContextOptions<ArriveDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Excursion> Excursions { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<FAQ> FAQs { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
