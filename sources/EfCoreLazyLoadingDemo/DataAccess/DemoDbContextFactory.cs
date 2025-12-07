using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.EfCoreLazyLoadingDemo.DataAccess;

internal class DemoDbContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
{
    public DemoDbContext CreateDbContext(string[] args)
    {
        ServiceCollection serviceCollection = new();
        Setup.ConfigureServices(serviceCollection);

        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        return serviceProvider.GetRequiredService<DemoDbContext>();
    }
}