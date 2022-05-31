using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models
{
    public class UserInfoSendToFront
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string token { get; set; }
        public int StatusCode { get; set; }
    }
}
