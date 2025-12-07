# 3) Lazy Loading of a Navigation Property (no `Include`)

Remove the `Include` statement added earlier to the Linq query.

## a) Include NuGet Package

```
Install-Package Microsoft.EntityFrameworkCore.Proxies
```

## b) Activate Lazy Loading

In the setup class, where the database context is configured:

```c#
serviceCollection.AddDbContext<DemoDbContext>(static options =>
{
    ...

    options.UseLazyLoadingProxies();
});
```

## c) Make the navigation properties virtual

Entity Framework needs to create proxy classes which inherit the entities and overwrite the navigation property's `get` accessor so that it has the chance to execute an additional SQL query on the database and retrieve the actual entity when the navigation property is used for the first time.

To be able to do all that, the navigation property must be virtual.

```c#
internal class Order
{
    ...

    public virtual Customer Customer { get; set; }
}
```

## d) Make the entities public...

In addition to the virtual property the entity classes themselves must be public for the proxy engine to work.

```c#
public class Order
{
	...
}
```

## d') ... or make the entire assembly visible to the proxy engine

If, for any reason, in your project doesn't make sense to make the entities public, the alternative is to allow the proxy engine (the `DynamicProxyGenAssembly2` assembly) to access the internal types. For that use the `InternalsVisibleTo` statement in the `.csproj` file:

```xml
<ItemGroup>
	<InternalsVisibleTo Include="DynamicProxyGenAssembly2"/>
</ItemGroup>
```

## e) Run

Run the application

> **SQL Server Profiler**
>
> - The initial SQL query brings only the orders.
>
> ```sql
> exec sp_executesql N'SELECT [o].[Id], [o].[CustomerId], [o].[Date]
> FROM [Orders] AS [o]
> WHERE [o].[Date] >= @startDate',N'@startDate datetime2(7)',@startDate='2023-01-01 00:00:00'
> ```
>
> - For each order, when the `Customer` (navigation property) is accessed the first time, an additional query to DB is executed.
>
> ```sql
> exec sp_executesql N'SELECT TOP(1) [c].[Id], [c].[Name]
> FROM [Customers] AS [c]
> WHERE [c].[Id] = @p',N'@p uniqueidentifier',@p='16DBD500-F562-4A9B-6DA4-08DE3594707B'
> ```
>

**Note**

The additional queries are performed once for each customer. If another order is referencing a customer already retrieved from the database, Entity Framework is smart enough to use that one.