using dr_heinekamp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models.Repositories
{
    public class SharedUserFilesRepo : ISharedUserFiles
    {
        private readonly AppDbContext _dbContext;
        
        public SharedUserFilesRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ShareLink_WithUsers> Add(ShareLink_WithUsers shareLink_WithUsers)
        {
            string path = Path.Combine("https://localhost:44383/api/download/download?userId="+shareLink_WithUsers.UserId+"&fileId="+shareLink_WithUsers.UserFilesId);
            if(shareLink_WithUsers!=null)
            {
                shareLink_WithUsers.Id = Guid.NewGuid().ToString();
                shareLink_WithUsers.ExpirationDate = DateTime.Now.AddDays(1);
                shareLink_WithUsers.DownloadURL = path;
                shareLink_WithUsers.Download = false;
                await _dbContext.ShareLink_WithUsers.AddAsync(shareLink_WithUsers);
                await _dbContext.SaveChangesAsync();
                return shareLink_WithUsers;
            }
            return null;
        }

        public bool Exist(string fileId, string userId)
        {
            var exist = _dbContext.ShareLink_WithUsers.SingleOrDefault(s => s.UserId == userId && s.UserFiles.Id == fileId);
            if(exist == null)
            {
                return false;
            }
            return true;
        }

        public async Task<ShareLink_WithUsers> Reshare(string fileId, string userId)
        {
            string path = Path.Combine("https://localhost:44383/api/download/download?userId=" + userId + "&fileId=" + fileId);
            var shared = _dbContext.ShareLink_WithUsers.SingleOrDefault(s => s.UserId == userId && s.UserFiles.Id == fileId);
            if (shared != null)
            {
                shared.ExpirationDate = DateTime.Now.AddDays(1);
                shared.DownloadURL = path;
                shared.Download = false;
                await _dbContext.SaveChangesAsync();
                return shared;
            }
            return null;
        }
    }
}
