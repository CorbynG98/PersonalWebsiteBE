using AutoMapper;
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
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public SessionController(ISessionService sessionService, IUserService userService, IMapper mapper)
        {
            this.sessionService = sessionService;
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        [AuthorizationFilter]
        public async Task<ActionResult> GetSessions()
        {
            // Get session token and verify
            var sessionToken = HttpContext.Request.Headers.Authorization.FirstOrDefault();
            var currentSession = await userService.GetSessionByToken(sessionToken);
            var sessions = await sessionService.GetSessionsForUser(currentSession.UserId);
            // Convert to resource so we can clean up response data a little
            var sessionsResource = mapper.Map<List<Session>, List<SessionResource>>(sessions);
            var currentSessionResource = sessionsResource.FirstOrDefault(t => t.Id == currentSession.Id);
            if (currentSessionResource != null) { currentSessionResource.CurrentSession = true; }
            var orderedResults = sessionsResource.OrderByDescending(t => t.CreatedAt);
            return Ok(orderedResults);
        }

        [HttpDelete("Revoke/{sessionId}")]
        [AuthorizationFilter]
        public async Task<ActionResult> RevokeSession(string sessionId)
        {
            // Get session token and verify the session being revoked is of the authed user
            var sessionToken = HttpContext.Request.Headers.Authorization.FirstOrDefault();
            var session = await userService.GetSessionByToken(sessionToken);
            var sessions = await sessionService.GetSessionsForUser(session.UserId);
            if (!sessions.Any(t => t.Id == sessionId)) return BadRequest("If this session exists, it is not yours to revoke.");
            // Revoke the session
            await sessionService.RevokeSessionById(sessionId, session.UserId, session.Id);
            return NoContent();
        }
    }
}
