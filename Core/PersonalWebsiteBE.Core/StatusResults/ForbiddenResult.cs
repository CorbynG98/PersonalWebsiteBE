using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.StatusResults
{
    public class ForbiddenResult : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult("Session token is not valid.")
            {
                StatusCode = 403
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
