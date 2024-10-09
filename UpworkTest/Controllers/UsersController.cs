using Application.DataTransferObjects;
using Application.IUser;
using Application.IUserDataAccess;
using Domain.User;
using Domain.UserData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UpworkTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userSerivceData;
        private readonly IUserDataAccessService _usersData;
        public UsersController(IUserService userSerivceData, IUserDataAccessService usersData)
        {
            _userSerivceData = userSerivceData;
            _usersData = usersData;
        }

        [HttpGet]
        public async Task<List<UsersDTO>> GetAsync()
        {
            return await _usersData.DecorateUsersDto(await _userSerivceData.GetAllUsersAsync());
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public async Task<UsersDTO> GetAsync(string id)
        {
            return await _userSerivceData.GetUserByIdAsync(id);
        }

        [HttpPost]
        public async Task<string> CreateAsync(UsersDTO user)
        {
            var data = await _userSerivceData.AddUserAsync(user);

            UsersDataAccessDTO userData = new();
            userData.UserId = data;
            userData.IsRequestedToDelete = false;
            userData.AllowedTill = DateTime.Now.AddDays(30);
            await _usersData.AddUserDataAccessAsync(userData);            

            return data;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<string> UpdateAsync(string id, UsersDTO user)
        {
            user.Id = id;
            return await _userSerivceData.EditUserAsync(user);            
        }

        [HttpDelete]
        public async Task<string> DeleteAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return "Id not found";
            }            
            await _userSerivceData.DeleteUserAsync(userId);
            return "Deleted successfully!!";
        }
    }
}