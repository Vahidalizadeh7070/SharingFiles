using dr_heinekamp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Classes.UserFiles_Classes
{
    public static class UploadUserFilesStatusCodes
    {
        public static int ReturnStatusCodes(UserFiles userFiles)
        {
            if (userFiles != null)
            {
                return StatusCodes.Status201Created;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }

        public static int ReturnStatusCodesForMultipleUploadFile(SubFiles subFiles)
        {
            if (subFiles != null)
            {
                return StatusCodes.Status201Created;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }

    }
}
