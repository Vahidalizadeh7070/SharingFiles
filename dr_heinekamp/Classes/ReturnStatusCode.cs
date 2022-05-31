using dr_heinekamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace dr_heinekamp.Classes
{
    public class ReturnStatusCode
    {
        private UserManager<ApplicationUser> _userManager;

        public ReturnStatusCode(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int> ReturnStatusCodes(string email, string password)
        {
            UserFunctions userFunctions = new UserFunctions(_userManager);
            if (await userFunctions.Exist(email) == true)
            {
                return StatusCodes.Status409Conflict;
            }
            else if (await userFunctions.CreateUser(email, password) == true)
            {
                return StatusCodes.Status201Created;
            }
            return StatusCodes.Status500InternalServerError;
        }
    }
}
