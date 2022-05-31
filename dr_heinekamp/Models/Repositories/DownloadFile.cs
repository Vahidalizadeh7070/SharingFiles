using dr_heinekamp.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace dr_heinekamp.Models.Repositories
{
    public class DownloadFile : IDownloadFile
    {
        private readonly AppDbContext _dbContext;
        private readonly HttpClient _httpClient;
        

        public DownloadFile(AppDbContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
        }
        public string Download(string userId, string fileId)
        {
            var model = _dbContext.ShareLink_WithUsers.Include(d=>d.UserFiles).SingleOrDefault(
                d => d.UserId == userId &&
                d.UserFilesId == fileId
            );

            if (model!=null && model.ExpirationDate.Day >= DateTime.Now.Day)
            {
                return model.UserFiles.File;
            }
            return null;
        }

        public string DownloadByMySelf(string userId, string fileName)
        {
            var model = _dbContext.UserFiles.SingleOrDefault(
                d => d.UserId == userId &&
                d.File == fileName
            );

            if (model != null)
            {
                return model.File;
            }
            return null;
        }

        public string DownloadSubFilesByMySelf(string userFilesId, string fileId)
        {
            var model = _dbContext.SubFiles.SingleOrDefault(
                d => d.UserFilesId == userFilesId &&
                d.Id == fileId
            );

            if (model != null)
            {
                return model.FileName;
            }
            return null;
        }

        public bool UpdateDownloadStatus(string userId, string fileId)
        {
            var model = _dbContext.ShareLink_WithUsers.SingleOrDefault(d=>d.UserId==userId&&d.UserFilesId==fileId);
            if(model!=null)
            {
                model.Download = true;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
