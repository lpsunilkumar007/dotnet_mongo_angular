using Infrastructure.Persistence;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.WorkerBackgroundService
{
    internal static class Startup
    {
        internal static IServiceCollection AddBackGroundService(this IServiceCollection services)
        {
            return services.AddHostedService<RunEveryMinute>()
                           .AddHostedService<RunMidNight>(); 
        }
    }
}
