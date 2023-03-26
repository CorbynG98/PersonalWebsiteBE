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
    public class ActivityMapper: Profile
    {
        public ActivityMapper()
        {
            // Model to resource mapping, and back again
            CreateMap<AuthActivity, AuthActivityResource>();
        }
    }
}
