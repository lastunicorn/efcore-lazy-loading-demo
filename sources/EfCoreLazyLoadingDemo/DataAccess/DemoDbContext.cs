using Microsoft.EntityFrameworkCore;

namespace DustInTheWind.EfCoreLazyLoadingDemo.DataAccess;

internal class DemoDbContext : DbContext
{
    public DemoDbContext(DbContextOptions<DemoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }
}
