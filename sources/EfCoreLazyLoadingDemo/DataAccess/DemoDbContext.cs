using DustInTheWind.EfCoreLazyLoadingDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace DustInTheWind.EfCoreLazyLoadingDemo.DataAccess;

internal class DemoDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DemoDbContext(DbContextOptions<DemoDbContext> options)
        : base(options)
    {
    }
}
