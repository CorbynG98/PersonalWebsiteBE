using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Services.Auth;
using PersonalWebsiteBE.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MiscController : Controller
    {
        private readonly IMapper mapper;
        private readonly IMiscService miscService;

        public MiscController(IMiscService miscService, IMapper mapper) {
            this.mapper = mapper;
            this.miscService = miscService;
        }

        [AllowAnonymous]
        [HttpGet("Heartbeat")]
        public ActionResult Heartbeat()
        {
            return Ok("Online, and healthy");
        }

        [AllowAnonymous]
        [HttpPost("LogPageView")]
        public async Task<ActionResult> LogPageView([FromBody] PageViewResource dataResource)
        {
            // Map to a friendly object
            var data = mapper.Map<PageViewResource, PageView>(dataResource);
            // Pass through to service to log it I guess
            await miscService.LogPageView(data);
            // Return ok to keep standards happy
            return Ok();
        }
    }
}
