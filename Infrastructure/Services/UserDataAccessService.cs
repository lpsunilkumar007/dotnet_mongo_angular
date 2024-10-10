using Application.DataTransferObjects;
using Application.IUserDataAccess;
using Domain.User;
using Domain.UserData;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Services
{
    public class UserDataAccessService : IUserDataAccessService
    {
        #region App DB Context
        private readonly ApplicationDbContext _dbContext;
        #endregion
        #region Constructor
        public UserDataAccessService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add User Data Access Async
        /// </summary>
        /// <param name="newUserDataAccessDto"></param>
        /// <returns></returns>
        public async Task<string> AddAsync(UsersDataAccessDTO newUserDataAccessDto)
        {
            UserDataAccess userDataAccess = new();
            userDataAccess.Id = ObjectId.GenerateNewId().ToString();
            userDataAccess.AllowedTill = newUserDataAccessDto.AllowedTill;
            userDataAccess.UserId = newUserDataAccessDto.UserId;
            userDataAccess.IsRequestedToDelete = newUserDataAccessDto.IsRequestedToDelete;

            await _dbContext.UserDataAccess.AddAsync(userDataAccess);
            await _dbContext.SaveChangesAsync();
            return userDataAccess.Id;
        }

        /// <summary>
        /// Remove User Data upon Consent
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> RemoveConsentedUserData(CancellationToken cancellationToken)
        {
            var toDeleteData = await _dbContext.UserDataAccess.Where(b => b.IsRequestedToDelete == true).ToListAsync();
            return await Delete(toDeleteData);
        }
        /// <summary>
        /// Remove Old User Data (More than 30 days)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> RemoveOldUserData(CancellationToken cancellationToken)
        {
            var toDeleteData = await _dbContext.UserDataAccess.Where(x => x.AllowedTill < DateTime.Now).ToListAsync();
            return await Delete(toDeleteData);
        }

        /// <summary>
        /// Private Method to delete data by service
        /// </summary>
        /// <param name="toDeleteData"></param>
        /// <returns></returns>
        private async Task<bool> Delete(List<UserDataAccess> toDeleteData)
        {
            List<Users> userData = new();
            if (toDeleteData.Count > 0)
            {
                foreach (var toDeleteDatum in toDeleteData)
                {
                    var data = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == toDeleteDatum.UserId);
                    if (data != null)
                        userData.Add(data);
                }
                _dbContext.Users.RemoveRange(userData);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Update Async
        /// </summary>
        /// <param name="updatedUser"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> EditAsync(string updatedUser)
        {
            var userToUpdate = await _dbContext.UserDataAccess.FirstOrDefaultAsync(b => b.UserId == updatedUser);
            if (userToUpdate != null)
            {
                userToUpdate.IsRequestedToDelete = true;
                _dbContext.UserDataAccess.Update(userToUpdate);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("user data access to be updated cannot be found");
            }
            return updatedUser;
        }

        /// <summary>
        /// Decorate user Object and add remaining days
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task<List<UsersDTO>> DecorateUsersDto(List<UsersDTO> users)
        {
            foreach (var user in users)
            {                
                var usersDataAccess = await _dbContext.UserDataAccess.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (usersDataAccess != null)
                {
                    user.RemainingDays = (usersDataAccess.AllowedTill.Date - DateTime.Now.Date).Days;
                    user.IsRequestedToDelete = usersDataAccess.IsRequestedToDelete;
                }
            }
            return users;
        }
        #endregion
    }
}