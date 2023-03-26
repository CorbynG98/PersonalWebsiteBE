using AutoMapper;
using Google.Cloud.Diagnostics.AspNetCore3;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteBE.Core.Constants;
using PersonalWebsiteBE.Core.Exceptions;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Helpers.Security;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Services.Auth;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.Filters;
using PersonalWebsiteBE.Repository.Repositories.Auth;
using PersonalWebsiteBE.Services.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly IActivityService activityService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public AuthController(ISessionService sessionService, IUserService userService, IActivityService activityService, IMapper mapper)
        {
            this.sessionService = sessionService;
            this.activityService = activityService;
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet("Sessions")]
        [AuthorizationFilter]
        public async Task<ActionResult> GetSessions(int page = 1, int pageSize = 10)
        {
            // Get session token and verify
            var sessionToken = HttpContext.Request.Headers.Authorization.FirstOrDefault();
            var currentSession = await sessionService.GetSessionByToken(sessionToken);
            var sessions = await sessionService.GetSessionsForUser(currentSession.UserId, (page - 1) * pageSize, pageSize + 1);
            // Convert to resource so we can clean up response data a little
            var sessionsResource = mapper.Map<List<Session>, List<SessionResource>>(sessions);
            var currentSessionResource = sessionsResource.FirstOrDefault(t => t.Id == currentSession.Id);
            if (currentSessionResource != null) { currentSessionResource.CurrentSession = true; }
            var orderedResults = sessionsResource.OrderByDescending(t => t.CurrentSession).ThenByDescending(t => t.CreatedAt);
            return Ok(orderedResults);
        }

        [HttpGet("AuthActivities")]
        [AuthorizationFilter]
        public async Task<ActionResult> GetAuthActivity(int page = 1, int pageSize = 10)
        {
            // Get session token and verify
            var sessionToken = HttpContext.Request.Headers.Authorization.FirstOrDefault();
            var currentSession = await sessionService.GetSessionByToken(sessionToken);
            var activities = await activityService.GetActivitiesByUserId(currentSession.UserId, (page-1)*pageSize, pageSize+1);
            // Convert to resource so we can clean up response data a little
            var activitiesResource = mapper.Map<List<AuthActivity>, List<AuthActivityResource>>(activities);
            var orderedResults = activitiesResource.OrderByDescending(t => t.ActionedAt);
            return Ok(orderedResults);
        }

        [HttpDelete("Sessions/Revoke/{sessionId}")]
        [AuthorizationFilter]
        public async Task<ActionResult> RevokeSession(string sessionId)
        {
            // Get session token and verify the session being revoked is of the authed user
            var sessionToken = HttpContext.Request.Headers.Authorization.FirstOrDefault();
            var session = await sessionService.GetSessionByToken(sessionToken);
            // Revoke the session
            await sessionService.RevokeSessionById(sessionId, session.UserId, session.Id);
            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromForm] AuthResource userResource)
        {
            var user = mapper.Map<AuthResource, User>(userResource);
            AuthData authData;
            try
            {
                authData = await userService.LoginUserAsync(user, GetIp());
            }
            catch (UserLoginException ex)
            {
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
            var result = await sessionService.VerifyUserSession(sessionToken);
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
