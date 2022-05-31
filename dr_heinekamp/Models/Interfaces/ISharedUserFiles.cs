using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models.Interfaces
{
    public interface ISharedUserFiles
    {
        Task<ShareLink_WithUsers> Add(ShareLink_WithUsers shareLink_WithUsers);
        bool Exist(string fileId, string userId);
        Task<ShareLink_WithUsers> Reshare(string fileId, string userId);
        //public int DownloadCount(string fileId);
    }
}
