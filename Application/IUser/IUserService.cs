using Application.DataTransferObjects;
using Domain.User;
using Survey.Api.Application.Common.Interfaces;

namespace Application.IUser
{
    public interface IUserService : ITransientService
    {
        Task<List<UsersDTO>> GetAllUsersAsync();
        Task<UsersDTO?> GetUserByIdAsync(string id);
        Task<string> AddUserAsync(UsersDTO newUser);
        Task<string> EditUserAsync(UsersDTO updatedUser);
        Task DeleteUserAsync(string userToDelete);
    }
}
