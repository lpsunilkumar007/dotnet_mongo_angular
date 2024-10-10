using Application.DataTransferObjects;
using Application.IUser;
using Domain.User;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Services
{
    public class UsersService : IUserService
    {
        #region AppDbContext
        private readonly ApplicationDbContext _users;
        #endregion
        #region Constructor
        public UsersService(ApplicationDbContext user)
        {
            _users = user;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Add User Async
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<string> AddAsync(UsersDTO newUser)
        {            
            Users user = new();
            user.Id = ObjectId.GenerateNewId().ToString();
            user.MobileNumber = newUser.MobileNumber;
            user.Email = newUser.Email;
            user.LastName = newUser.LastName;
            user.FirstName= newUser.FirstName;

            await _users.Users.AddAsync(user);
            await _users.SaveChangesAsync();           

            return user.Id;
        }
        /// <summary>
        /// Delete User Async
        /// </summary>
        /// <param name="userToDelete"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task SoftDeleteAsync(string userToDelete)
        {
            var reservationToDelete = await _users.Users.FirstOrDefaultAsync(b => b.Id == userToDelete);

            if (reservationToDelete != null)
            {               
                reservationToDelete.IsDeleted = true;
                _users.Users.Update(reservationToDelete);
                await _users.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("The user to delete cannot be found.");
            }
        }

        /// <summary>
        /// Edit User Async
        /// </summary>
        /// <param name="updatedUser"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> EditAsync(UsersDTO updatedUser)
        {
            var userToUpdate = await _users.Users.FirstOrDefaultAsync(b => b.Id == updatedUser.Id);

            if (userToUpdate != null)
            {
                userToUpdate.MobileNumber = updatedUser.MobileNumber;
                userToUpdate.Email = updatedUser.Email;
                userToUpdate.LastName = updatedUser.LastName;
                userToUpdate.FirstName = updatedUser.FirstName;                               
                _users.Users.Update(userToUpdate);
                await _users.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("user to be updated cannot be found");
            }
            return updatedUser.Id;
        }

        /// <summary>
        /// Get All Users Async
        /// </summary>
        /// <returns></returns>
        public async Task<List<UsersDTO>> GetAllAsync()
        {
            var users = await _users.Users.Where(x => x.IsDeleted == false).OrderBy(c => c.FirstName).AsNoTracking().Select(x => new UsersDTO
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MobileNumber = x.MobileNumber,
                Id = x.Id,
                IsDeleted = x.IsDeleted
            }).ToListAsync();
            return users;
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UsersDTO> GetIdAsync(string id)
        {
            var data = await _users.Users.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            UsersDTO userDto = new UsersDTO();
            userDto.Id = data.Id;
            userDto.FirstName = data.FirstName;
            userDto.LastName = data.LastName;
            userDto.MobileNumber = data.MobileNumber;
            userDto.Email = data.Email;
            return userDto;
        }
        #endregion
    }
}