using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.DTOs
{
    public class AddSubFilesDTO
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public IList<IFormFile> UploadFile { get; set; }
        public string UserFilesId { get; set; }
    }
}
