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
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();

            var services = new ServiceCollection();

            ConfigureServices(services, config);

            services.AddSingleton<StartUp, StartUp>()
                .BuildServiceProvider()
                .GetService<StartUp>()
                .Run();
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