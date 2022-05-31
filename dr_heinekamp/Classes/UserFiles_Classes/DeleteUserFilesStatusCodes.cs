using dr_heinekamp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Classes.UserFiles_Classes
{
    public static class DeleteUserFilesStatusCodes
    {
        public static int ReturnStatusCodes(UserFiles userFiles)
        {
            if(userFiles!= null)
            {
                UploadFiles.Delete(userFiles.File);
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }

        public static int ReturnStatusCodesSubFiles(SubFiles subFiles)
        {
            if (subFiles != null)
            {
                UploadFiles.DeleteSubFiles(subFiles.FileName);
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }
    }
}
