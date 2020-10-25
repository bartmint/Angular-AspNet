using API.ViewModels;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDTO, AppUser>();
            CreateMap<UserForRegisterDTO, UserDTO>();
            CreateMap<AppUser, UserDTO>();
        }
    }
}
