using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Core
{
    [FirestoreData]
    public class Email : Document
    {
        [FirestoreProperty]
        public string From { get; set; }
        [FirestoreProperty]
        public string Fullname { get; set; }
        [FirestoreProperty]
        public string Subject { get; set; }
        [FirestoreProperty]
        public string Content { get; set; }
        [FirestoreProperty]
        public string Status { get; set; }
        [FirestoreProperty]
        public string StatusMessage { get; set; }
    }
}
