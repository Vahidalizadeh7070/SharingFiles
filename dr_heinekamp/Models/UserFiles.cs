using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models
{
    public class UserFiles
    {
        public string Id { get; set; }
        public string Title { get;set; }
        [NotMapped]
        public IFormFile UploadFile { get; set; }
        public string File { get;set; }
        public DateTime UploadDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public IEnumerable<SubFiles> SubFiles { get; set; }
    }
}
