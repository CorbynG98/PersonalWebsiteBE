using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Auth
{
    [FirestoreData]
    public class Session : Document
    {
        [FirestoreProperty]
        public string UserId { get; set; }
        [FirestoreProperty]
        public string SessionToken { get; set; } // Hashed
    }
}
