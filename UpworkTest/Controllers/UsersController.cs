using Application.DataTransferObjects;
using Application.IUser;
using Application.IUserDataAccess;
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
        #region Properties
        private readonly IUserService _userSerivceData;
        private readonly IUserDataAccessService _usersData;
        #endregion
        #region Constructor
        public UsersController(IUserService userSerivceData, IUserDataAccessService usersData)
        {
            _userSerivceData = userSerivceData;
            _usersData = usersData;
        }
        #endregion
        /// <summary>
        /// Get Async All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UsersDTO>> GetAsync()
        {
            return await _usersData.DecorateUsersDto(await _userSerivceData.GetAllAsync());
        }

        /// <summary>
        /// Get Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public async Task<UsersDTO> GetAsync(string id)
        {
            return await _userSerivceData.GetIdAsync(id);
        }
        /// <summary>
        /// Create Async
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> CreateAsync(UsersDTO user)
        {
            var data = await _userSerivceData.AddAsync(user);

            UsersDataAccessDTO userData = new();
            userData.UserId = data;
            userData.IsRequestedToDelete = false;
            userData.AllowedTill = DateTime.Now.AddDays(30);
            await _usersData.AddAsync(userData);            

            return data;
        }
        /// <summary>
        /// Update Async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<string> UpdateAsync(string id, UsersDTO user)
        {
            user.Id = id;
            return await _userSerivceData.EditAsync(user);            
        }
        /// <summary>
        /// Delete Async
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<string> DeleteAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return "Id not found";
            }            
            await _userSerivceData.SoftDeleteAsync(userId);
            return "Deleted successfully!!";
        }
    }
}