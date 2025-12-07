using DustInTheWind.EfCoreLazyLoadingDemo.DataAccess;
using DustInTheWind.EfCoreLazyLoadingDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace DustInTheWind.EfCoreLazyLoadingDemo.UseCases;

internal class DisplayOrdersUseCase
{
    private readonly DemoDbContext demoDbContext;

    public DisplayOrdersUseCase(DemoDbContext demoDbContext)
    {
        this.demoDbContext = demoDbContext ?? throw new ArgumentNullException(nameof(demoDbContext));
    }

    internal async Task ExecuteAsync()
    {
        DateTime startDate = new(2023, 1, 1);

        IQueryable<Order> query = demoDbContext.Orders
            .Where(x => x.Date >= startDate);

        //List<Order> orders = await query.ToListAsync();

        DisplayOrder(query);
    }

    private static void DisplayOrder(IEnumerable<Order> orders)
    {
        foreach (Order order in orders)
        {
            Console.WriteLine($"Order");
            Console.WriteLine($"  - Id: {order.Id}");
            Console.WriteLine($"  - Date: {order.Date}");
            Console.WriteLine($"  - Customer Id: {order.CustomerId}");

            // Access "Customer" - navigation property
            if (order.Customer != null)
            {
                Console.WriteLine($"  - Customer");
                Console.WriteLine($"    - Id: {order.Customer.Id}");
                Console.WriteLine($"    - Id: {order.Customer.Name}");
            }
            else
            {
                Console.WriteLine("  - Customer: <null>");
            }

            Console.WriteLine();
        }
    }
}