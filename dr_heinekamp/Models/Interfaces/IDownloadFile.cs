using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace dr_heinekamp.Models.Interfaces
{
    public interface IDownloadFile
    {
        string Download(string userId, string fileId);
        string DownloadByMySelf(string userId, string fileName);
        bool UpdateDownloadStatus(string userId, string fileId);
        string DownloadSubFilesByMySelf(string userFilesId, string fileId);
    }
}
