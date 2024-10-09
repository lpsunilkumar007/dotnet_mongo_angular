using Application.DataTransferObjects;
using Application.IUser;
using Domain.User;
using Domain.UserData;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Services
{
    public class UsersService : IUserService
    {
        private readonly ApplicationDbContext _users;
        public UsersService(ApplicationDbContext user)
        {
            _users = user;
        }

        public async Task<string> AddUserAsync(UsersDTO newUser)
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

        public async Task DeleteUserAsync(string userToDelete)
        {
            var reservationToDelete = await _users.Users.FirstOrDefaultAsync(b => b.Id == userToDelete);

            if (reservationToDelete != null)
            {
                _users.Users.Remove(reservationToDelete);
                await _users.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("The user to delete cannot be found.");
            }
        }

        public async Task<string> EditUserAsync(UsersDTO updatedUser)
        {
            var userToUpdate = await _users.Users.FirstOrDefaultAsync(b => b.Id == updatedUser.Id);

            if (userToUpdate != null)
            {
                userToUpdate.Update(updatedUser.FirstName, updatedUser.Email, updatedUser.LastName, updatedUser.MobileNumber);
                _users.Users.Update(userToUpdate);
                await _users.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("user to be updated cannot be found");
            }
            return updatedUser.Id;
        }

        public async Task<List<UsersDTO>> GetAllUsersAsync()
        {
            var users = await _users.Users.OrderBy(c => c.FirstName).AsNoTracking().Select(x => new UsersDTO
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MobileNumber = x.MobileNumber,
                Id = x.Id
            }).ToListAsync();
            return users;
        }

        public async Task<UsersDTO> GetUserByIdAsync(string id)
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
    }
}