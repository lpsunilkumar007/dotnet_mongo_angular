using Application.DataTransferObjects;
using Application.IUser;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UpworkTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCompositeController : ControllerBase
    {
        #region Properties

        private readonly IUserCompoundIndexService _usersData;
        #endregion
        #region Constructor
        public UserCompositeController(IUserCompoundIndexService usersData)
        {            
            _usersData = usersData;
        }
        #endregion
        /// <summary>
        /// Get Async All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UsersDTO>> GetAsync(string firstName, string lastName)
        {
            return await _usersData.GetByNameAsync(firstName);
        }
    }
}
