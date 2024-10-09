using Application.IUserDataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.WorkerBackgroundService
{
    public class RunEveryMinute : BackgroundService
    {
        private readonly ILogger<RunEveryMinute> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RunEveryMinute(ILogger<RunEveryMinute> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {                   
                    using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                    {
                        IUserDataAccessService scopedProcessingService =
                            scope.ServiceProvider.GetRequiredService<IUserDataAccessService>();
                        var result = await scopedProcessingService.RemoveConsentedUserData(stoppingToken);
                        if(result)
                        {
                            _logger.LogInformation("Database operation completed successfully at: {time}", DateTimeOffset.Now);
                        }
                        else
                        {
                            _logger.LogError("Could not delete all the data from the database at: {time}", DateTimeOffset.Now);
                        }
                    }                   
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while accessing the database at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Run every minute
            }
        }

    }
}