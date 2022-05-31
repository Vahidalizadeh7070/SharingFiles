using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models
{
    public class SubFiles
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string UserFilesId { get; set; }
        public UserFiles UserFiles { get; set; }
    }
}
