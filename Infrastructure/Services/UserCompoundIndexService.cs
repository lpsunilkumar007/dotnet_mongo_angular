using Application.DataTransferObjects;
using Application.IUser;
using Domain.User;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.MongoDbService;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Services
{
    public class UserCompoundIndexService : IUserCompoundIndexService
    {
        #region AppDbContext
        private readonly ApplicationDbContext _users;
        private readonly MongoDbProviderService _mongoDbService;
        #endregion
        #region Constructor
        public UserCompoundIndexService(ApplicationDbContext user, MongoDbProviderService mongoDbService)
        {
            _users = user;
            _mongoDbService = mongoDbService;
        }
        #endregion
        #region Methods       

        /// <summary>
        /// Get User By Name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<UsersDTO>> GetByNameAsync(string searchText)
        {
            try
            {
                var userCol = _mongoDbService.GetUsersCollection();

                var sort = Builders<Users>.Sort.Ascending(m => m.FirstName).Ascending(m => m.LastName);

                var projection = Builders<Users>.Projection
                    .Include(m => m.FirstName)
                    .Include(m => m.LastName)
                    .Include(m => m.Email)
                    .Include(m => m.MobileNumber);
                List<BsonDocument> results = new();
                if (string.IsNullOrEmpty(searchText))
                {
                    var filterEmpty = Builders<Users>.Filter.Empty;
                    results = await userCol.Find(filterEmpty).Project(projection).ToListAsync();
                }
                else
                {
                    var filter = Builders<Users>.Filter.Or(
                        Builders<Users>.Filter.Regex(
                            field => field.FirstName,
                            new MongoDB.Bson.BsonRegularExpression(searchText, "i")
                        ),
                       Builders<Users>.Filter.Regex(
                            field => field.LastName,
                            new MongoDB.Bson.BsonRegularExpression(searchText, "i")
                        )
                    );
                    results = await userCol.Find(filter).Sort(sort).Project(projection).ToListAsync();
                }

                List<UsersDTO> users = new();

                foreach (var result in results)
                {
                    UsersDTO user = new UsersDTO();
               
                    user.LastName = result["LastName"].ToString();
                    user.FirstName = result["FirstName"].ToString();
                    user.Email = result["Email"].ToString();
                    user.MobileNumber = result["MobileNumber"].ToString();
                    users.Add(user);
                }
               
                return users;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}