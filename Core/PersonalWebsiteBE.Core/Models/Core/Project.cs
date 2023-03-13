using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Core
{
    [FirestoreData]
    public class Project : Document
    {
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string Source { get; set; } // Source code, if available
        [FirestoreProperty]
        public string Description { get; set; }
        [FirestoreProperty]
        public string ImageUrl { get; set; } // Storage public image url
        [FirestoreProperty]
        public string LiveUrl { get; set; } // Live url if available
        [FirestoreProperty]
        public bool Featured { get; set; } // Show this seperatly on front end.
        [FirestoreProperty]
        public List<string> TechStack { get; set; } // List of languages used on the project
    }
}
