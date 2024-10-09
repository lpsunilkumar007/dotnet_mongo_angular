using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    internal static class Startup
    {
        internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            var mongoDBSettings = config.GetSection("UsersDatabaseSettings").Get<MongoDBSettings>();                       
            return services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMongoDB(mongoDBSettings.ConnectionString ?? "", mongoDBSettings.DatabaseName ?? ""));
        }        
    }
}
