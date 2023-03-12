using Google.Cloud.Firestore;
using PersonalWebsiteBE.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Auth
{
    [FirestoreData]
    public class AuthActivity : Document
    {
        [FirestoreProperty]
        public AuthActivityType Type { get; set; }
        [FirestoreProperty]
        public string UserId { get; set; }
        [FirestoreProperty]
        public string SessionId { get; set; }
        [FirestoreProperty]
        public DateTime? ActionedAt { get; set; }
    }
}
