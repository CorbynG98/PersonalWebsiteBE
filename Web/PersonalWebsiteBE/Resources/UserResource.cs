using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Auth
{
    public class UserResource
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
