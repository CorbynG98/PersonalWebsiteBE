using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Auth
{
    public class AuthResource
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class UserDataResource
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Username { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}
