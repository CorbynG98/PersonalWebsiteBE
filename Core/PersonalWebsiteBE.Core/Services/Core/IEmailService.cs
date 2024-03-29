﻿using PersonalWebsiteBE.Core.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Services.Core
{
    public interface IEmailService : IService<EmailLog>
    {
        Task<bool> CreateNewMessageEmail(EmailLog email);
    }
}
