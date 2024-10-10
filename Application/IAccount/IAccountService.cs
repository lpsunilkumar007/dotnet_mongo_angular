using Application.DataTransferObjects;
using Survey.Api.Application.Common.Interfaces;

namespace Application.IAccount
{
    public interface IAccountService : ITransientService
    {
        Task<string> AddAsync(RegisterDTO newUser);
    }
}
