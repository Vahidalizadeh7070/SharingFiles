using dr_heinekamp.Helper;
using dr_heinekamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Classes
{
    public class UserLoggedIn
    {
        private HttpContext _httpContext;
        private UserManager<ApplicationUser> _userManager;
        private JWTService _jwtService;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;

        public UserLoggedIn(HttpContext httpContext, UserManager<ApplicationUser> userManager, JWTService jWTService, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _httpContext = httpContext;
            _userManager = userManager;
            _jwtService = jWTService;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<UserInfoSendToFront> Info()
        {
            UserInfoSendToFront userinfo = new UserInfoSendToFront();
            // Get cookie 
            var jwt = _httpContext.Request.Cookies["jwt"];

            // If the cookie is null then return Unauthorized
            if (jwt == null)
            {
                return userinfo ;
            }

            // Verify the jwt token
            var token = _jwtService.Verify(jwt);

            // Find the email 
            string email = token.Claims.FirstOrDefault(c => c.Issuer == "User").Value;

            // Retrieve all information of a user and assign it to user variable
            var user = await _userManager.FindByEmailAsync(email);
            userinfo.StatusCode = StatusCodes.Status200OK;            
            userinfo.UserId = user.Id;
            userinfo.Email = user.Email;
            // Return Ok and pass the user object 
            return userinfo;
        }
    }
}
