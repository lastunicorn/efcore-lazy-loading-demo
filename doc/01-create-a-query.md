# 1) Create a Query

## a) Create a query to retrieve Orders

```c#
IQueryable<Order> query = demoDbContext.Orders
    .Where(x => x.Date >= startDate);
```

The `IQueriable` instance contains only the description of the data that is needed. It does not actually contains the data. At this point in time, no actual SQL request was performed on the database.

> **SQL Server Profiler**
>
> - No request is performed on the database, yet.

## b) Materialize the query

Materializing the Linq query means that the `IQueriable` instance is translated into a SQL query and executed on the database.

To materialize the Linq query call one of the following methods:

- `ToList`, `ToArray`, `ToDictionary`,

- `First`, `FirstOrDefault`
- `Single`, `SingleOrDefault`
- etc.

**Example**

```c#
List<Order> orders = await query.ToListAsync();
```

> **SQL Server Profiler**
>
> - A request is executed on the database, retrieving the orders, but no customers.
>
> ```sql
> exec sp_executesql N'SELECT [o].[Id], [o].[CustomerId], [o].[Date]
> FROM [Orders] AS [o]
> WHERE [o].[Date] >= @startDate',N'@startDate datetime2(7)',@startDate='2023-01-01 00:00:00'
> ```

## c) Access the navigation property

Iterate through the list of orders to display them.

```c#
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
```

At some point the `Customer` navigation property is accessed:

- The `Customer` (navigation property) is always `null`, even if there is a `CustomerId` and the customer exists in the database. The navigation property is not automatically populated.

> **SQL Server Profiler**
>
> - No additional request is performed on the database.