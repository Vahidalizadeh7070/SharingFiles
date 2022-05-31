using AutoMapper;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Profiles
{
    public class SharedUserFilesProfile : Profile
    {
        public SharedUserFilesProfile()
        {
            // Source --> Target
            CreateMap<ShareLink_WithUsers, SharedUserFilesDTO>();
            CreateMap<SharedUserFilesDTO,ShareLink_WithUsers>();
        }
    }
}
