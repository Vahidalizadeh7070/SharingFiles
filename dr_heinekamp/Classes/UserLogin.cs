using dr_heinekamp.Helper;
using dr_heinekamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace dr_heinekamp.Classes
{
    public class UserLogin
    {
        private HttpContext _httpContext;
        private UserManager<ApplicationUser> _userManager;
        private JWTService _jwtService;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;

        public UserLogin(HttpContext httpContext, UserManager<ApplicationUser> userManager, JWTService jWTService, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _httpContext = httpContext;
            _userManager = userManager;
            _jwtService = jWTService;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<(string,string)> StatusCode(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);

            // If the user is not null and the pass is correct then the if block will be executed
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                // Get user roles
                var userRoles = await _userManager.GetRolesAsync(user);

                // Add user the claim
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };

                // Add role and user to the Claim type
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // Generate a token
                var authSignKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Tokens:Key"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Audience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                    );
                var tokenCookie = new JwtSecurityTokenHandler().WriteToken(token);

                // Assign token to the cookie
                _httpContext.Response.Cookies.Append("jwt", tokenCookie, new CookieOptions
                {
                    HttpOnly = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(1)
                });

                // Return Ok 
                return (tokenCookie,user.Id);
            }
            return (null,null);
        }
    }
}
