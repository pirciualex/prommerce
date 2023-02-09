using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Prommerce.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("PROMMERCE_DB_CONNECTION_STRING");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<Db>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }
    }
}