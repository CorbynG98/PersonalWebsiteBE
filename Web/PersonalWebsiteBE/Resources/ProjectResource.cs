using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Auth
{
    public class ProjectResource
    {
        public string Name { get; set; }
        public string Source { get; set; } 
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string LiveUrl { get; set; }
        public string Stars { get; set; }
        public bool IsDescriptionMarkdown { get; set; }
        public bool Featured { get; set; }
        public List<string> TechStack { get; set; }
    }
}
