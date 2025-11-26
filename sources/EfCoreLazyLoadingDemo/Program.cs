using DustInTheWind.EfCoreLazyLoadingDemo.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DustInTheWind.EfCoreLazyLoadingDemo;

internal static class Program
{
    private static void Main(string[] args)
    {
        const string connectionString = "Server=localhost;Database=EfLazyLoadingDemo;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True";

        DbContextOptions<DemoDbContext> options = new DbContextOptionsBuilder<DemoDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        using DemoDbContext demoDbContext = new(options);
        
        //foreach (Customer customer in demoDbContext.Customers)
        //{
        //    Console.WriteLine($"Customer: {customer.Name}");
        //}

        foreach (Order order in demoDbContext.Orders)
        {
            Console.WriteLine($"Order: {order.ProductName}");
        }
    }
}
