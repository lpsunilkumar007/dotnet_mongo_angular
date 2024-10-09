using Application.DataTransferObjects;
using Domain.User;
using Domain.UserData;
using Survey.Api.Application.Common.Interfaces;

namespace Application.IUserDataAccess
{
    public interface IUserDataAccessService : ITransientService
    {
        Task<List<UsersDataAccessDTO>> GetAllUsersDataAccessAsync();
        Task<UsersDataAccessDTO?> GetUserDataAccessByIdAsync(string id);
        Task<string> AddUserDataAccessAsync(UsersDataAccessDTO newUser);
        Task<string> EditUserDataAccessAsync(UsersDataAccessDTO updatedUser);
        Task<string> EditUserDataAccessByUserIdAsync(UsersDTO updatedUser);
        Task DeleteUserDataAccessAsync(string userToDelete);
        Task<bool> RemoveConsentedUserData(CancellationToken cancellationToken);
        Task<bool> RemoveOldUserData(CancellationToken cancellationToken);
        Task<List<UsersDTO>> DecorateUsersDto(List<UsersDTO> users);
    }
}
