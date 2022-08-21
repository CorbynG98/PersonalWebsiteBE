using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Auth
{
    [FirestoreData]
    public class User : Document
    {
        [FirestoreProperty]
        public string Username { get; set; }
        [FirestoreProperty]
        public string Password { get; set; }
        [FirestoreProperty]
        public string ResetPasswordToken { get; set; }
        [FirestoreProperty]
        public DateTime? LastLoginAt { get; set; }
        [FirestoreProperty]
        public DateTime? UpdatedAt { get; set; }
    }
}
