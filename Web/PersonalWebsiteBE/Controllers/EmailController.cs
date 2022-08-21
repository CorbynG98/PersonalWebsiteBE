using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : Controller
    {
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        public EmailController(IMapper mapper, IEmailService emailService) {
            this.mapper = mapper;
            this.emailService = emailService;
        }

        [HttpPost("Contact")]
        public async Task<IActionResult> SendContactEmail(EmailResource emailResource) {
            // Map to nicer model
            var email = mapper.Map<EmailResource, EmailLog>(emailResource);
            // Get service to send the email
            var emailSent = await emailService.CreateNewMessageEmail(email);
            // Let user know of the response
            if (emailSent) return Ok("Email has been sent");
            return BadRequest("Email failed to send! Unsure why!");
        }
    }
}
