using Application.DataTransferObjects;
using Survey.Api.Application.Common.Interfaces;

namespace Application.IUser
{
    public interface IUserCompoundIndexService : ITransientService
    {
        #region Methods        
        /// <summary>
        /// Get By first Name or Last Name
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        Task<List<UsersDTO?>> GetByNameAsync(string searchText);        
        #endregion
    }
}
