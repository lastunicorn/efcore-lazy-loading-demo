namespace DustInTheWind.EfCoreLazyLoadingDemo.Domain;

internal class Order
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public Guid CustomerId { get; set; }

    public virtual Customer Customer { get; set; }
}
