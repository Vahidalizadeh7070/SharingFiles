using dr_heinekamp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Classes.UserFiles_Classes
{
    public static class DetailsUserFilesStatusCodes
    {
        public static int ReturnStatusCodes(UserFiles userFiles)
        {
            if (userFiles != null)
            {
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status404NotFound;
            }
        }
        public static int ReturnStatusCodesSubFiles(IEnumerable<SubFiles> subFiles)
        {
            if (subFiles.Count()>0)
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
