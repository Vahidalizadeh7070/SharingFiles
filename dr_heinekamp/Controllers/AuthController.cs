using dr_heinekamp.Classes;
using dr_heinekamp.DTOS;
using dr_heinekamp.Helper;
using dr_heinekamp.Models;
using dr_heinekamp.Models.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace dr_heinekamp.Controllers
{
    // AuthController
    // This controller is responsible for Authentication and Authorization 
    // Register and login of a user perform in this controller
    // We use Asp.net Identity and combine it with JWT
    // JWT provides a token based on the user and then you can pass the token Authorize all users using the token
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Fields that are going to use throughout the controller

        private UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTService _JWTService;
        private readonly IConfiguration _configuration;

        // Constructor
        public AuthController(UserManager<ApplicationUser> userManager, JWTService jWTService, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _JWTService = jWTService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            ReturnStatusCode userFunc= new ReturnStatusCode(_userManager);
            var statusCode = await userFunc.ReturnStatusCodes(model.Email,model.Password);
            return StatusCode(statusCode,new { Message = statusCode== 201 ? "Your sign up successfully " : "Failed" });
        }

        // Login action
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            UserLogin userLogin = new UserLogin(HttpContext,_userManager,_JWTService,_roleManager,_configuration);
            var statusCode = await userLogin.StatusCode(model.Email, model.Password);
            if(statusCode.Item1!=null && statusCode.Item2!=null)
            { 
            return Ok(new {token=statusCode.Item1, email = model.Email, userId = statusCode.Item2});
            }
            else
            {
                return BadRequest();
            }
        }

        // Users action
        // This action check the token and Authorize the user base on the token value
        [HttpGet(template: "users")]
        public async Task<IActionResult> Users()
        {
            UserLoggedIn userLoggedIn = new UserLoggedIn(HttpContext,_userManager,_JWTService,_roleManager,_configuration);
            var value = await userLoggedIn.Info();
            return Ok(value.token);
        }
    }
}