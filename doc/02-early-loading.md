# 2) Early Loading of a Navigation Property (with `Include`)

Add the `Include` statement in the Linq query.

```c#
IQueryable<Order> query = demoDbContext.Orders
    .Include(x => x.Customer)
    .Where(x => x.Date >= startDate);
```

This change will instruct Entity Framework to retrieve all the associated `Customer` instances.

> **SQL Server Profiler**
>
> - A single SQL request is executed on the database which retrieves all the orders and associated customers
>
> ```sql
> exec sp_executesql N'SELECT [o].[Id], [o].[CustomerId], [o].[Date], [c].[Id], [c].[Name]
> FROM [Orders] AS [o]
> INNER JOIN [Customers] AS [c] ON [o].[CustomerId] = [c].[Id]
> WHERE [o].[Date] >= @startDate',N'@startDate datetime2(7)',@startDate='2023-01-01 00:00:00'
> ```

### 