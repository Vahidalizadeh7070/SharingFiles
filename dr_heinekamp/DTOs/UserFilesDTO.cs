using dr_heinekamp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.DTOs
{
    public class UserFilesDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string File { get; set; }
        public DateTime UploadDate { get; set; }
        public string UserId { get; set; }
    }
}
