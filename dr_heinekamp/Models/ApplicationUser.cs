using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models
{
    // This class inherits from IdentityUser 
    public class ApplicationUser:IdentityUser
    {
        // We can add new properties to the Identity user and then it will be added to the AspnetUser table
    }
}
