using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.MongoDbService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Persistence
{
    internal static class Startup
    {
        internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            var mongoDBSettings = config.GetSection("UsersDatabaseSettings").Get<MongoDBSettings>();
            var connectionString = mongoDBSettings.ConnectionString;

            return services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMongoDB(mongoDBSettings.ConnectionString ?? "", mongoDBSettings.DatabaseName ?? ""));
           
        }
        internal static IServiceCollection AddMongoDBService(this IServiceCollection services, IConfiguration config)
        {
            var mongoDBSettings = config.GetSection("UsersDatabaseSettings").Get<MongoDBSettings>();

            var connectionString = mongoDBSettings.ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(mongoDBSettings.DatabaseName);
            var collection = database.GetCollection<BsonDocument>("users");

            var indexDefinition = new BsonDocument
            {
                { "FirstName", "text" },
                { "LastName", "text" }
            };
            var indexModel = new CreateIndexModel<BsonDocument>(indexDefinition);


            collection.Indexes.CreateOne(indexModel);
            services.Configure<MongoDBSettings>(config.GetSection("UsersDatabaseSettings"));
            return services.AddSingleton<MongoDbProviderService>();
        }    
    }
}
