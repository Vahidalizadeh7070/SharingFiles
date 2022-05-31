using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.DTOs
{
    public class SharedUserFilesDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserFilesId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DownloadURL { get; set; }
        public bool Download { get; set; }
    }
}
