using Domain.User;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoDbService
{
    public class MongoDbProviderService
    {
        private readonly IMongoDatabase _database;
        /// <summary>
        /// Configure Provider Service
        /// </summary>
        /// <param name="settings"></param>
        public MongoDbProviderService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Users> GetUsersCollection()
        {
            return _database.GetCollection<Users>("users");
        }
    }
}
