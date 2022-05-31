using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models
{
    public class ShareLink_WithUsers
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserFilesId { get; set; }
        public DateTime ExpirationDate { get; set; }
        [Required]
        public string DownloadURL { get; set; }
        public bool Download { get; set; }
        public ApplicationUser User { get; set; }
        public UserFiles UserFiles { get; set; }
    }
}
