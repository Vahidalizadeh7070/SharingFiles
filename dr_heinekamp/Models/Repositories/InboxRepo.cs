using dr_heinekamp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models.Repositories
{
    public class InboxRepo : IInbox
    {
        private readonly AppDbContext _dbContext;

        public InboxRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int DownloadCount(string fileId)
        {
            return _dbContext.ShareLink_WithUsers.Where(x => x.UserFilesId == fileId && x.Download == true).Count();
        }

        public IEnumerable<ShareLink_WithUsers> List(string userId)
        {
            return _dbContext.ShareLink_WithUsers.OrderByDescending(i=>i.ExpirationDate).Include(i => i.UserFiles).ThenInclude(i => i.User)
                .Where(i => i.UserId == userId).ToList();
        }
    }
}
