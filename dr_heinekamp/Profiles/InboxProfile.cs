using AutoMapper;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Profiles
{
    public class InboxProfile:Profile
    {
        public InboxProfile()
        {
            // Source --> Target
            CreateMap<ShareLink_WithUsers, InboxDTO>().ForMember(dest=>dest.SenderUserName, dest => dest.MapFrom(src => src.UserFiles.User.Email))
                .ForMember(dest => dest.FileName, dest => dest.MapFrom(src => src.UserFiles.File))
                .ForMember(dest => dest.Title, dest => dest.MapFrom(src=>src.UserFiles.Title));
        }
    }
}
