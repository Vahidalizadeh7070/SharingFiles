using dr_heinekamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Classes
{
    public class Validation_UserId
    {
        private readonly AppDbContext _dbContext;

        public Validation_UserId(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool ValidateUser(string userId)
        {
            var valid = _dbContext.Users.SingleOrDefault(u => u.Id == userId);
            if(valid != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
