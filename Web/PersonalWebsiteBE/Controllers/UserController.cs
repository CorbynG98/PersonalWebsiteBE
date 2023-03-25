using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteBE.Core.Exceptions;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Services.Auth;
using PersonalWebsiteBE.Filters;
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
        private readonly IMapper mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
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
            var session = await userService.GetUserBySessionToken(sessionToken);
            var user = await userService.GetByIdAsync(session.Id);
            // Map to resource :)
            var userResource = mapper.Map<User, UserDataResource>(user);
            return Ok(userResource);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromForm] AuthResource userResource)
        {
            var user = mapper.Map<AuthResource, User>(userResource);
            AuthData authData;
            try
            {
                authData = await userService.LoginUserAsync(user, GetIp());
            } catch (UserLoginException ex) {
                return BadRequest(ex.Message);
            }
            // Return session in 200 response
            return Ok(authData);
        }

        [HttpPost("Logout")]
        [AuthorizationFilter]
        public async Task<ActionResult> LogoutAsync()
        {
            var sessionToken = HttpContext.Request.Headers.Authorization.FirstOrDefault();
            await userService.LogoutUserAsync(sessionToken);
            return NoContent();
        }

        [HttpPost("VerifySession")]
        public async Task<ActionResult> VerifySession()
        {
            var sessionToken = HttpContext.Request.Headers.Authorization.FirstOrDefault();
            if (sessionToken == null) return Ok(false);
            var result = await userService.VerifyUserSession(sessionToken);
            return Ok(result);
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
