using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Classes
{
    public static class UploadFiles
    {
        public static string Upload(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserFiles", fileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        public static string UploadMultiple(IFormFile file)
        {

            var fileName =file.FileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserFiles","SubFiles", fileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        public static bool Delete(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserFiles", fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }

        public static bool DeleteSubFiles(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserFiles","SubFiles", fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
