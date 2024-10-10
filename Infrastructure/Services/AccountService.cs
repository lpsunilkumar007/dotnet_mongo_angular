using Application.DataTransferObjects;
using Application.IAccount;
using Domain.User;
using Infrastructure.Persistence.Context;
using MongoDB.Bson;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _dbContext;
        public AccountService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Add User Async
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<string> AddAsync(RegisterDTO newUser)
        {
            Users user = new();
            user.Id = ObjectId.GenerateNewId().ToString();
            user.MobileNumber = newUser.Phone;
            user.Email = newUser.Email;
            user.LastName = newUser.LastName;
            user.FirstName = newUser.FirstName;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }
    }
}
