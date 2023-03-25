using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            // Model to resource mapping, and back again
            CreateMap<AuthResource, User>().ReverseMap();
            CreateMap<UserDataResource, User>().ReverseMap();
        }
    }
}
