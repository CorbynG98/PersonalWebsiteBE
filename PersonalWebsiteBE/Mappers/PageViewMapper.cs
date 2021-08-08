using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Mappers
{
    public class PageViewMapper : Profile
    {
        public PageViewMapper()
        {
            // Model to resource mapping, and back again
            CreateMap<PageViewResource, PageView>();
        }
    }
}
