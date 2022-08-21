using Google.Cloud.Firestore;
using PersonalWebsiteBE.Core.Constants;

namespace PersonalWebsiteBE.Core.Models.Core
{
    [FirestoreData]
    public class EmailTemplate : Document
    {
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public EmailTemplateTypes TemplateType { get; set; }
        [FirestoreProperty]
        public string SubjectLine { get; set; }
        [FirestoreProperty]
        public string HtmlContent { get; set; }
    }
}
