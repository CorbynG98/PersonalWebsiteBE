using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteBE.Core.Exceptions;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Services.Auth;
using PersonalWebsiteBE.Filters;
using PersonalWebsiteBE.Services.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ISessionService sessionService;
        private readonly IMapper mapper;
        public UserController(IUserService userService, ISessionService sessionService, IMapper mapper)
        {
            this.userService = userService;
            this.sessionService = sessionService;
            this.mapper = mapper;
        }

        [HttpPost]
        [AuthorizationFilter]
        public async Task<ActionResult> CreateAsync([FromBody] AuthResource userResource)
        {
            // Convert resource to model
            var user = mapper.Map<AuthResource, User>(userResource);
            // Pass through to service
            var authData = await userService.CreateUserAsync(user, GetIp());
            // Return sessin in 200 response
            return Ok(authData);
        }

        [HttpGet]
        [AuthorizationFilter]
        public async Task<ActionResult> GetProfileData()
        {
            var sessionToken = HttpContext.Request.Headers.Authorization.FirstOrDefault();
            var session = await sessionService.GetUserBySessionToken(sessionToken);
            var user = await userService.GetByIdAsync(session.Id);
            // Map to resource :)
            var userResource = mapper.Map<User, UserDataResource>(user);
            return Ok(userResource);
        }

        private string GetIp()
        {
            var ip = this.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = this.Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            return ip;
        }
    }
}
