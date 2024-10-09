using Application.DataTransferObjects;
using Application.IUserDataAccess;
using Domain.User;
using Domain.UserData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UpworkTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersDataAccessController : ControllerBase
    {
        private readonly IUserDataAccessService _userDataAccessService;
        public UsersDataAccessController(IUserDataAccessService userDataAccessService)
        {
            _userDataAccessService = userDataAccessService;
        }

        [HttpGet]
        public async Task<List<UsersDataAccessDTO>> GetAsync()
        {
            return await _userDataAccessService.GetAllUsersDataAccessAsync();
        }

        [HttpGet("{id:length(24)}", Name = "GetUserDataAsync")]
        public async Task<UsersDataAccessDTO> GetAsync(string id)
        {
            return await _userDataAccessService.GetUserDataAccessByIdAsync(id);
        }

        [HttpPost]
        public async Task<string> CreateAsync(UsersDataAccessDTO userDataAccess)
        {
            return await _userDataAccessService.AddUserDataAccessAsync(userDataAccess);
            
        }

        [HttpPut("{id:length(24)}")]
        public async Task<string> UpdateAsync(string id, UsersDataAccessDTO userDataAccess)
        {
            userDataAccess.Id = id;
            return await _userDataAccessService.EditUserDataAccessAsync(userDataAccess);            
        }
        [HttpPut]
        [Route("UpdateByUserIdAsync")]
        public async Task<string> UpdateByUserIdAsync(string id, UsersDTO user)
        {
            return await _userDataAccessService.EditUserDataAccessByUserIdAsync(user);
        }
        [HttpDelete]
        public async Task<string> DeleteAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return "Id not found";
            }            
            await _userDataAccessService.DeleteUserDataAccessAsync(userId);
            return "Deleted successfully!!";
        }
    }
}