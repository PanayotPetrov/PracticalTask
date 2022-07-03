using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticalTask.Data;
using PracticalTask.Data.Common;
using PracticalTask.Data.Common.Repositories;
using PracticalTask.Data.Repositories;
using PracticalTask.Services.Data;

namespace PracticalTask.Services.WorkerService
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();

            var services = new ServiceCollection();

            ConfigureServices(services, config);

            var app = ConfigureApp(services);

            await app.RunAsync();
        }

        private static StartUp ConfigureApp(IServiceCollection services)
        {

            var dbContext = services.BuildServiceProvider()
                .GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();

            var app = services.AddSingleton<StartUp, StartUp>()
                    .BuildServiceProvider()
                    .GetService<StartUp>();

            return app;
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            services.AddScoped<IGuidModelService, GuidModelService>();
        }
    }
}