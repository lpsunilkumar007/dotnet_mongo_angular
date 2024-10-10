using Application.DataTransferObjects;
using Application.IUserDataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UpworkTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersDataAccessController : ControllerBase
    {
        #region Properties
        private readonly IUserDataAccessService _userDataAccessService;
        #endregion
        #region Constructor
        public UsersDataAccessController(IUserDataAccessService userDataAccessService)
        {
            _userDataAccessService = userDataAccessService;
        }
        #endregion

        /// <summary>
        /// Update by User Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateByUserIdAsync")]
        public async Task<string> UpdateByUserIdAsync(string id, UsersDTO user)
        {
            return await _userDataAccessService.EditAsync(id);
        }
    }
}