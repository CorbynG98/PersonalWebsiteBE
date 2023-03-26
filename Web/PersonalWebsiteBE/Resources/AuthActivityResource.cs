using Google.Cloud.Firestore;
using PersonalWebsiteBE.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Auth
{
    public class AuthActivityResource
    {
        public AuthActivityType Type { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public DateTime? ActionedAt { get; set; }
    }
}
