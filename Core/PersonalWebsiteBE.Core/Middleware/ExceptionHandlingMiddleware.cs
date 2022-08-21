using Google.Cloud.Diagnostics.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsiteBE.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, [FromServices] IExceptionLogger exceptionLogger)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case UserLoginException:
                        await HandleCommonException(response, ex.Message, (int)HttpStatusCode.Unauthorized);
                        break;
                    default:
                        exceptionLogger.Log(ex, context);
                        await HandleCommonException(response, ex.Message, (int)HttpStatusCode.InternalServerError);
                        break;
                }
            }
        }

        private static async Task HandleCommonException(HttpResponse response, string message, int statusCode)
        {
            if (!response.HasStarted)
            {
                var errorResponse = new
                {
                    Message = message,
                    StatusCode = statusCode
                };
                response.StatusCode = statusCode;
                var errorJson = JsonConvert.SerializeObject(errorResponse);
                await response.WriteAsync(errorJson);
            }
            else
            {
                await response.WriteAsync(string.Empty);
            }
        }
    }
}
