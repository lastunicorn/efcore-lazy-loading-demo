using DustInTheWind.EfCoreLazyLoadingDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace DustInTheWind.EfCoreLazyLoadingDemo.DataAccess;

internal class DbSeeding
{
    private readonly string[] firstNames = ["Alice", "Bob", "Charlie", "Diana", "Ethan", "Fiona", "George", "Hannah", "Ian", "Julia"];
    private readonly string[] lastNames = ["Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez"];

    private readonly DbContext dbContext;

    public DbSeeding(DbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void Execute()
    {
        List<Customer> customers = CreateCustomers();
        CreateOrders(customers);

        dbContext.SaveChanges();
    }

    private List<Customer> CreateCustomers()
    {
        bool customersExists = dbContext.Set<Customer>()
            .Any();

        if (customersExists)
            return [];

        List<Customer> customers = Enumerable.Range(0, 3)
            .Select(_ =>
            {
                string firstName = firstNames[Random.Shared.Next(firstNames.Length)];
                string lastName = lastNames[Random.Shared.Next(lastNames.Length)];

                Customer customer = new()
                {
                    Name = firstName + " " + lastName
                };

                return customer;
            })
            .ToList();

        dbContext.Set<Customer>()
            .AddRange(customers);

        return customers;
    }

    private void CreateOrders(List<Customer> customers)
    {
        bool ordersExists = dbContext.Set<Order>()
            .Any();

        if (ordersExists)
            return;

        if (customers.Count == 0)
            return;

        int seconds100Days = 100 * 24 * 60 * 60;

        for (int i = 0; i < 10; i++)
        {
            int customerIndex = Random.Shared.Next(customers.Count);
            Customer customer = customers
                .Skip(customerIndex)
                .First();

            Order order = new()
            {
                Date = DateTime.UtcNow.AddSeconds(-Random.Shared.Next(seconds100Days)),
                Customer = customer
            };

            dbContext.Set<Order>()
                .Add(order);
        }
    }
}