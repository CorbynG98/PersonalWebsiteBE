using AutoMapper;
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
    public class ProjectController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public ProjectController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpPost]
        [AuthorizationFilter]
        public async Task<ActionResult> CreateAsync([FromBody] UserResource userResource)
        {
            // Convert resource to model
            var user = mapper.Map<UserResource, User>(userResource);
            // Pass through to service
            var authData = await userService.CreateUserAsync(user);
            // Return sessin in 200 response
            return Ok(authData);
        }

        [HttpPost("GetProjects")]
        public async Task<ActionResult> GetProjects()
        {
            throw new NotImplementedException();
        }
    }
}
