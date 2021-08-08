﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonalWebsiteBE.Core.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace PersonalWebsiteBE.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext actionContext)
        {
            var userService = (IUserService)actionContext.HttpContext.RequestServices.GetService(typeof(IUserService));
            // Try get the session token from header of request
            var sessionToken = actionContext.HttpContext.Request.Headers.FirstOrDefault(t => t.Key == "SessionToken").Value.ToString();
            if (String.IsNullOrWhiteSpace(sessionToken))
            {
                actionContext.Result = new Core.StatusResults.UnauthorizedResult();
                return;
            }
            // Pass this token through to firestore query to check if it exists. Return true if it does exist
            if (!await userService.VerifyUserSession(sessionToken)) {
                actionContext.Result = new Core.StatusResults.ForbiddenResult();
                return;
            }
        }
    }
}
