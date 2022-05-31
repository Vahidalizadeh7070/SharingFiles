using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.DTOs
{
    public class DownloadDTO
    {
        public string UserId { get; set; }
        public string FileId { get; set; }
        public string URL { get; set; }
    }
}
