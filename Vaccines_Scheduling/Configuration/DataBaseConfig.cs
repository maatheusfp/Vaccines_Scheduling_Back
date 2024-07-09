using Microsoft.EntityFrameworkCore;
using Vaccines_Scheduling.Repository;

namespace Vaccines_Scheduling.Configuration
{
    public static class DataBaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
