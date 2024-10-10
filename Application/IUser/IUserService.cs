using Application.DataTransferObjects;
using Survey.Api.Application.Common.Interfaces;

namespace Application.IUser
{
    public interface IUserService : ITransientService
    {
        #region Methods
        Task<List<UsersDTO>> GetAllAsync();
        Task<UsersDTO?> GetIdAsync(string id);
        Task<string> AddAsync(UsersDTO newUser);
        Task<string> EditAsync(UsersDTO updatedUser);
        Task DeleteAsync(string userToDelete);
        #endregion
    }
}
