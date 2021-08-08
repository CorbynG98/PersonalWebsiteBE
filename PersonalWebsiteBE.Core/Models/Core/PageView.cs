using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Core
{
    [FirestoreData]
    public class PageView : Document
    {
        [FirestoreProperty]
        public string Origin { get; set; }
        [FirestoreProperty]
        public int ViewCount { get; set; }
        [FirestoreProperty]
        public DateTime? LastVisited { get; set; }
    }
}
