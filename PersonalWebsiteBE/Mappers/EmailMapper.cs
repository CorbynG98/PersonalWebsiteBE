using AutoMapper;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Mappers
{
    public class EmailMapper : Profile
    {
        public EmailMapper()
        {
            // Model to resource mapping, and back again
            CreateMap<EmailResource, Email>();
        }
    }
}
