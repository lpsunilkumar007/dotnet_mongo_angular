using Application.DataTransferObjects;
using Application.IAccount;
using Application.IUserDataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UpworkTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region PROP
        private readonly IAccountService _accountService;
        private readonly IUserDataAccessService _usersData;
        #endregion

        #region CTOR
        public AccountController(IAccountService accountService, IUserDataAccessService usersData)
        {
            _accountService = accountService;
            _usersData = usersData;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO userModel)
        {
            if (userModel == null)
            {
                return BadRequest("Invalid user data.");
            }
            var id = await _accountService.AddAsync(userModel);
            UsersDataAccessDTO userData = new();
            userData.UserId = id;
            userData.IsRequestedToDelete = false;
            userData.AllowedTill = DateTime.Now.AddDays(30);
            await _usersData.AddAsync(userData);

            return Ok(new { message = "User registered successfully." });
        }
        #endregion
    }
}

