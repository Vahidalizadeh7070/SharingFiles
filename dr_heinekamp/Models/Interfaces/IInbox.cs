using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models.Interfaces
{
    public interface IInbox
    {
        IEnumerable<ShareLink_WithUsers> List(string userId);
        int DownloadCount(string fileId);
    }
}
