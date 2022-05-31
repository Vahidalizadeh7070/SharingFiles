using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dr_heinekamp.Models;

namespace dr_heinekamp.Classes.UserFilesClasses
{
    public static class ListUserFilesStatusCodes
    {
        public static int ReturnStatusCodes(IEnumerable<UserFiles> userFiles)
        {
            if (userFiles.Any())
            {
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status404NotFound;
            }
        }
    }
}
