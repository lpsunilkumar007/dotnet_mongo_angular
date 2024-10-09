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
        private readonly ApplicationDbContext _userDataAccess;

        public UserDataAccessService(ApplicationDbContext userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public async Task<string> AddUserDataAccessAsync(UsersDataAccessDTO newUserDataAccessDto)
        {
            UserDataAccess userDataAccess = new();
            userDataAccess.Id = ObjectId.GenerateNewId().ToString();
            userDataAccess.AllowedTill = newUserDataAccessDto.AllowedTill;
            userDataAccess.UserId = newUserDataAccessDto.UserId;
            userDataAccess.IsRequestedToDelete = newUserDataAccessDto.IsRequestedToDelete;

            await _userDataAccess.UserDataAccess.AddAsync(userDataAccess);
            await _userDataAccess.SaveChangesAsync();
            return userDataAccess.Id;
        }

        public async Task<bool> RemoveConsentedUserData(CancellationToken cancellationToken)
        {
            var toDeleteData = await _userDataAccess.UserDataAccess.Where(b => b.IsRequestedToDelete == true).ToListAsync();
            return await DeleteUserData(toDeleteData);
        }
        public async Task<bool> RemoveOldUserData(CancellationToken cancellationToken)
        {
            var toDeleteData = await _userDataAccess.UserDataAccess.Where(x => x.AllowedTill < DateTime.Now).ToListAsync();
            return await DeleteUserData(toDeleteData);
        }
        private async Task<bool> DeleteUserData(List<UserDataAccess> toDeleteData)
        {
            List<Users> userData = new();
            if (toDeleteData.Count > 0)
            {
                foreach (var toDeleteDatum in toDeleteData)
                {
                    var data = await _userDataAccess.Users.FirstOrDefaultAsync(u => u.Id == toDeleteDatum.UserId);
                    if (data != null)
                        userData.Add(data);
                }
                _userDataAccess.Users.RemoveRange(userData);
                var result = await _userDataAccess.SaveChangesAsync();
                return result > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }
        public async Task DeleteUserDataAccessAsync(string userDataAccessToDelete)
        {
            var reservationToDelete = await _userDataAccess.UserDataAccess.FirstOrDefaultAsync(b => b.Id == userDataAccessToDelete);

            if (reservationToDelete != null)
            {
                _userDataAccess.UserDataAccess.Remove(reservationToDelete);
                await _userDataAccess.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("The user data access to delete cannot be found.");
            }
        }

        public async Task<string> EditUserDataAccessAsync(UsersDataAccessDTO updatedUser)
        {
            var userToUpdate = await _userDataAccess.UserDataAccess.FirstOrDefaultAsync(b => b.Id == updatedUser.Id);

            if (userToUpdate != null)
            {
                userToUpdate.Update(updatedUser.UserId, updatedUser.AllowedTill, updatedUser.IsRequestedToDelete);
                _userDataAccess.UserDataAccess.Update(userToUpdate);
                await _userDataAccess.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("user data access to be updated cannot be found");
            }
            return updatedUser.Id;
        }
        public async Task<string> EditUserDataAccessByUserIdAsync(UsersDTO updatedUser)
        {
            var userToUpdate = await _userDataAccess.UserDataAccess.FirstOrDefaultAsync(b => b.UserId == updatedUser.Id);
            if (userToUpdate != null)
            {
                //userToUpdate.Update(string.Empty, DateTime.MinValue, true);
                userToUpdate.IsRequestedToDelete = true;
                _userDataAccess.UserDataAccess.Update(userToUpdate);
                await _userDataAccess.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("user data access to be updated cannot be found");
            }
            return updatedUser.Id;
        }
        public async Task<List<UsersDataAccessDTO>> GetAllUsersDataAccessAsync()
        {
            return await _userDataAccess.UserDataAccess.OrderByDescending(c => c.Id).AsNoTracking().Select(x => new UsersDataAccessDTO
            {
                AllowedTill = x.AllowedTill,
                IsRequestedToDelete = x.IsRequestedToDelete,
                Id = x.Id,
                UserId = x.UserId
            }).ToListAsync();
        }

        public async Task<UsersDataAccessDTO> GetUserDataAccessByIdAsync(string id)
        {
            var data = await _userDataAccess.UserDataAccess.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            UsersDataAccessDTO userDataAccessdto = new();
            userDataAccessdto.AllowedTill = data.AllowedTill;
            userDataAccessdto.Id = data.Id;
            userDataAccessdto.UserId = data.UserId;
            userDataAccessdto.IsRequestedToDelete = data.IsRequestedToDelete;
            return userDataAccessdto;
        }

        public async Task<List<UsersDTO>> DecorateUsersDto(List<UsersDTO> users)
        {
            foreach (var user in users)
            {                
                var usersDataAccess = await _userDataAccess.UserDataAccess.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (usersDataAccess != null)
                    user.RemainingDays = (usersDataAccess.AllowedTill.Date - DateTime.Now.Date).Days;
            }
            return users;
        }
    }
}