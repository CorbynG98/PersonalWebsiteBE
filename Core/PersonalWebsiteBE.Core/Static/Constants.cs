﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Constants
{
    public enum AuthActivityType { 
        Login,
        Logout,
        SessionRevoked
    }

    public enum EmailTemplateTypes
    {
        NewMessage = 0,
        PasswordReset = 1
    }
}
