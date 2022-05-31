using AutoMapper;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Classes.ListOfUsers
{
    public static class ListOfUsersStatusCode
    {
        public static int ReturnStatusCodes(IEnumerable<ApplicationUser> user)
        {
            if (!user.Any())
            {
                return StatusCodes.Status204NoContent;
            }
            else
            {
                
                return StatusCodes.Status200OK;
            }
        }
    }
}
