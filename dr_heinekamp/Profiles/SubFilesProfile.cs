using AutoMapper;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Profiles
{
    public class SubFilesProfile:Profile
    {
        public SubFilesProfile()
        {
            // Source --> Target
            CreateMap<SubFiles, SubFilesDTO>();
            CreateMap<SubFilesDTO, SubFiles>();
            CreateMap<SubFiles, AddSubFilesDTO>();
            CreateMap<AddSubFilesDTO,SubFiles>();
        }
    }
}
