namespace DustInTheWind.EfCoreLazyLoadingDemo.DataAccess;

internal class Order
{
    public Guid Id { get; set; }

    public string ProductName { get; set; }

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; }
}
