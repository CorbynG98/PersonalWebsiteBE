using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models
{
    public interface IDocument
    {
        public string Id { get; set; }
        DateTime CreatedAt { get; set; }
    }

    public class Document : IDocument
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
