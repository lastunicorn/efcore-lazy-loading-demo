# EF Core Lazy Loading Tutorial

## Navigation Property

A nice feature of Entity Framework is the navigation property which allows a parent entity to reference a child entity.

Parent entity:

```c#
internal class Order
{
    ...

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; }
}
```

Child entity:

```c#
internal class Customer
{
    ...
}
```

The `Order.CustomerId` property holds the reference id (Foreign Key in the database) that points to a customer.

The `Order.Customer` property it is called a navigation property which may hold the entire `Customer` instance, retrieved from a different table from the database.

The navigation properties are not populated by default by Entity Framework.

## How is `Customer` property populated

There are two approaches to populate them:

- Early loading (using `Include` statement)
- Lazy loading (using the Lazy Loading engine from Entity Framework)

## Tutorial

To understand these two approaches, check out the following steps:

- [0) Prerequisites](00-prerequisites.md)
- [1) Create a Query](01-create-a-query.md)
- [2) Early Loading](02-early-loading.md)
- [3) Lazy Loading](03-lazy-loading.md)

As a bonus, an interesting situation that may appear while using lazy loading, which may result in an error, is described here:

- [4) Multiple Data Readers](04-multiple-data-readers.md)