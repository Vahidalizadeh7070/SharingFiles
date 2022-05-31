using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models.Interfaces
{
    public interface IUsers
    {
        IEnumerable<ApplicationUser> UserList();
    }
}
