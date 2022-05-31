using AutoMapper;
using dr_heinekamp.Classes.ListOfUsers;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _users;
        private readonly IMapper _mapper;

        public UsersController(IUsers users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsersListDTO>> List()
        {
            var users = _users.UserList();
            var statusCode = ListOfUsersStatusCode.ReturnStatusCodes(users);
            return StatusCode(statusCode,new
            {
                value = statusCode == 200 ? _mapper.Map<IEnumerable<UsersListDTO>>(users) : null
            });
        }
        
    }
}
