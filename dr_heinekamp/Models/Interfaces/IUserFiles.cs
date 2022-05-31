using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models.Interfaces
{
    public interface IUserFiles
    {
        Task<UserFiles> Upload(UserFiles userFiles, string fileName);
        Task<SubFiles> UploadMultiple(SubFiles subFiles, string fileName);
        Task<UserFiles> Delete(string id, string userId);
        IEnumerable<UserFiles> List(string userId);
        int DownloadCount(string fileId);
        UserFiles Details(string id, string userId);

        IEnumerable<SubFiles> ListOfSubFiles(string fileId);

        Task<SubFiles> DeleteSubFiles(string id, string userFilesId);
    }
}
