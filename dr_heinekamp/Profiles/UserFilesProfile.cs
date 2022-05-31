using AutoMapper;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Profiles
{
    public class UserFilesProfile: Profile
    {
        public UserFilesProfile()
        {
            // Source --> Target
            CreateMap<UserFiles, UserFilesDTO>();
            CreateMap<UserFiles_UploadDTOs,UserFiles>();
            CreateMap<UserFilesDTO, UserFiles>();
        }
    }
}
