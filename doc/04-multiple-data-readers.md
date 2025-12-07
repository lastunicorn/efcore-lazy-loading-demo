# 4) Open Multiple Data Readers

By default SQL Server doesn't allow opening an additional `DbDataReader` while another one is still opened.

To force Entity Framework to open an additional `DbDataReader` , let's access a lazy loaded navigation property during the execution of the initial query.

## The previous materialization approach

We cannot use anymore the methods that materialize the Linq query, like `ToListAsync()` , because those methods, while opening a `DbDataReader` during their execution, they iterate through all the response, build the entity instances in memory, create the list containing those entities and, before returning that list, they close the `DbDataReader`.

When we, later, iterate through the results, we do it on the in-memory list.

## The new materialization approach

To have more flexibility to reproduce the problem (multiple `DbDataReader` instances), We need to materialize the query in a different way. We'll do so by iterating directly on the `IQueriable`. This will allow us to access the `Customer` navigation property while the previous request is still iterating through the response records.

```c#
internal async Task ExecuteAsync()
{
    IQueryable<Order> query = demoDbContext.Orders
        .Where(x => x.Date >= startDate);

    DisplayOrder(query);
}

private static void DisplayOrder(IEnumerable<Order> orders)
{
    foreach (Order order in orders)
    {
        ...

        // Access "Customer" - navigation property
        if (order.Customer != null)
        {
            ...
        }
        
        ...
    }
}
```

In this example the `foreach` statement forces the Entity Framework to execute the SQL query, but the `DbDataReader` instance remains active during the entire execution of the `foreach`. Before each step of the `foreach`, Entity Framework is using the initial `DbDataReader` to retrieve the next `Order` instance.

Now, what do you think will happen if we access the navigation property `Order.Customer` during the iteration?

```
System.InvalidOperationException: 'There is already an open DataReader associated with this Connection which must be closed first.'
```

## Allow multiple instances of `DbDataReader`

I wouldn't recommend the above approach in a real application.

Still, if for any reason, it is absolutely necessary to access a second reader there is a configuration flag that can be set in the connection string, to allow multiple data readers: `MultipleActiveResultSets=True`

An example of am entire connection string would be:

```
Server=localhost;Database=EfLazyLoadingDemo;Trusted_Connection=true;TrustServerCertificate=True;MultipleActiveResultSets=True
```

