using dr_heinekamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Classes
{
    public class UserFunctions
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserFunctions(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public  async Task<bool> Exist(string email)
        {
            // Find any users 
            var userExist = await _userManager.FindByNameAsync(email);

            // If the user exist in the context then return 500 Status code
            if (userExist != null)
            {
                return true;
                //return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "failed", Message = "User already exist" });
            }
            return false;
        }

        public  void AddRoleToUser(ApplicationUser user)
        {
            if (! _userManager.IsInRoleAsync(user, "User").Result)
            {
                _userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        public  async Task<bool> CreateUser (string email,string password)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = email
            };

            var result = await _userManager.CreateAsync(user, password);
            if(result.Succeeded)
            {
                AddRoleToUser(user);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
