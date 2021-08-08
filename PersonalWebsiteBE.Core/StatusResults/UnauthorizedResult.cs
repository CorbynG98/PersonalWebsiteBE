using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.StatusResults
{
    public class UnauthorizedResult : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult("No session token was found in the request.")
            {
                StatusCode = 401
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
