using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteBE.Core.Models.Auth;
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
            CreateMap<UserResource, User>();
        }
    }
}
