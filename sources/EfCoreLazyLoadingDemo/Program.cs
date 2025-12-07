using DustInTheWind.EfCoreLazyLoadingDemo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.EfCoreLazyLoadingDemo;

internal static class Program
{
    private static void Main(string[] args)
    {
        ServiceCollection serviceCollection = new();
        Setup.ConfigureServices(serviceCollection);

        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        // Instantiate and execute a use case.
        // ...
    }
}
