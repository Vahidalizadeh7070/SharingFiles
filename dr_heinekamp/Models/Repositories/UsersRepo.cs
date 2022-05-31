using dr_heinekamp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models.Repositories
{
    public class UsersRepo : IUsers
    {
        private readonly AppDbContext _dbContext;

        public UsersRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<ApplicationUser> UserList()
        {
            return _dbContext.Users.Where(x=>x.UserName != "admin@admin.com").ToList();
        }
    }
}
