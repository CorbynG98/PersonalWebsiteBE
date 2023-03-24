﻿using AutoMapper;
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
    public class SessionMapper: Profile
    {
        public SessionMapper()
        {
            // Model to resource mapping, and back again
            CreateMap<Session, SessionResource>();
        }
    }
}
