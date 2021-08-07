using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteBE.Core.Exceptions;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Services.Auth;
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
        public async Task<ActionResult> CreateAsync([FromBody] UserResource userResource)
        {
            // Convert resource to model
            var user = mapper.Map<UserResource, User>(userResource);
            // Pass through to service
            var authData = await userService.CreateUserAsync(user);
            // Return sessin in 200 response
            return Ok(authData);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromForm] UserResource userResource)
        {
            var user = mapper.Map<UserResource, User>(userResource);
            AuthData authData;
            try
            {
                authData = await userService.LoginUserAsync(user);
            } catch (UserLoginException ex) {
                return BadRequest(ex.Message);
            }
            // Return sessin in 200 response
            return Ok(authData);
        }

        [HttpPost("Logout")]
        public async Task<ActionResult> LogoutAsync()
        {
            var sessionToken = Request.Headers["SessionToken"].ToString();
            await userService.LogoutUserAsync(sessionToken);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task UpdateAsync(string id, [FromBody]UserResource userResource)
        {
            // await userService.TestingGoogleFirestore();
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            // await userService.TestingGoogleFirestore();
        }
    }
}
