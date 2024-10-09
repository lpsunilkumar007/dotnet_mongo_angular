using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.WorkerBackgroundService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddPersistence(config)
                .AddServices()
                .AddBackGroundService();                
        }
    }
}
