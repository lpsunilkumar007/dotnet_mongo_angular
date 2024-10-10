using Application.DataTransferObjects;
using Survey.Api.Application.Common.Interfaces;

namespace Application.IUserDataAccess
{
    public interface IUserDataAccessService : ITransientService
    { 
        Task<string> AddAsync(UsersDataAccessDTO newUser);
        Task<string> EditAsync(string updatedUser);        
        Task<bool> RemoveConsentedUserData(CancellationToken cancellationToken);
        Task<bool> RemoveOldUserData(CancellationToken cancellationToken);
        Task<List<UsersDTO>> DecorateUsersDto(List<UsersDTO> users);
    }
}
