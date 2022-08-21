using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Core
{
    [FirestoreData]
    public class EmailLog : Document
    {
        [FirestoreProperty]
        public string Content { get; set; }
        [FirestoreProperty]
        public string Subject { get; set; }
        [FirestoreProperty]
        public string From { get; set; }
        [FirestoreProperty]
        public string FromName { get; set; }
        [FirestoreProperty]
        public string To { get; set; }
        [FirestoreProperty]
        public string ToName { get; set; }
    }
}
