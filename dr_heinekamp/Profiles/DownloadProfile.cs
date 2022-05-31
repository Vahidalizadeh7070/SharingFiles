using AutoMapper;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Profiles
{
    public class DownloadProfile:Profile
    {
        public DownloadProfile()
        {
            // Source --> Target
            CreateMap<ShareLink_WithUsers, DownloadDTO>();
        }
    }
}
