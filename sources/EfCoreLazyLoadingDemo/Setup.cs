using DustInTheWind.EfCoreLazyLoadingDemo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.EfCoreLazyLoadingDemo;

internal static class Setup
{
    public static void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<DemoDbContext>(static options =>
        {
            const string connectionString = "Server=localhost;Database=EfCoreLazyLoadingDemo;Trusted_Connection=true;TrustServerCertificate=True";
            options.UseSqlServer(connectionString);

            options.UseSeeding((dbContext, _) =>
            {
                DbSeeding dbSeeding = new(dbContext);
                dbSeeding.Execute();
            });
        });
    }
}
