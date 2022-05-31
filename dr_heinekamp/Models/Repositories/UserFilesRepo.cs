using dr_heinekamp.Classes;
using dr_heinekamp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models.Repositories
{
    public class UserFilesRepo : IUserFiles
    {
        private readonly AppDbContext _dbContext;

        public UserFilesRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserFiles> Delete(string id, string userId)
        {
            var model = _dbContext.UserFiles.SingleOrDefault(uF => uF.Id == id && uF.UserId == userId);
            if (model != null)
            {
                _dbContext.UserFiles.Remove(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            return null;
        }

        public UserFiles Details(string id, string userId)
        {
            return _dbContext.UserFiles.SingleOrDefault(uF => uF.Id == id && uF.UserId == userId);
        }
        public int DownloadCount(string fileId)
        {
            return _dbContext.ShareLink_WithUsers.Include(d => d.UserFiles).Where(d => d.UserFiles.Id == fileId && d.Download == true).Count();
        }

        public IEnumerable<UserFiles> List(string userId)
        {
            return _dbContext.UserFiles.OrderByDescending(uF => uF.UploadDate).Where(uF => uF.UserId == userId);
        }

        public async Task<UserFiles> Upload(UserFiles userFiles, string fileName)
        {
            var ValidUserId = new Validation_UserId(_dbContext);
            if (ValidUserId.ValidateUser(userFiles.UserId))
            {
                userFiles.UploadDate = DateTime.Now;
                userFiles.Id = Guid.NewGuid().ToString();
                userFiles.File = fileName;
                await _dbContext.UserFiles.AddAsync(userFiles);
                await _dbContext.SaveChangesAsync();
                return userFiles;
            }
            return null;
        }

        public async Task<SubFiles> UploadMultiple(SubFiles subFiles, string fileName)
        {
            subFiles.Id = Guid.NewGuid().ToString();
            subFiles.FileName = fileName;
            await _dbContext.SubFiles.AddAsync(subFiles);
            await _dbContext.SaveChangesAsync();
            return subFiles;
        }

        public IEnumerable<SubFiles> ListOfSubFiles(string fileId)
        {
            return _dbContext.SubFiles.Where(s => s.UserFilesId == fileId).ToList();
        }

        public async Task<SubFiles> DeleteSubFiles(string id, string userFilesId)
        {
            var model = _dbContext.SubFiles.SingleOrDefault(uF => uF.Id == id && uF.UserFilesId== userFilesId);
            if (model != null)
            {
                _dbContext.SubFiles.Remove(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            return null;
        }
    }
}
