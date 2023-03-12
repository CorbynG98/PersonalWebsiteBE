using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Settings
{
    public interface IFireStoreSettings
    {
        string ProjectId { get; set; }
        string DefaultStorageBucket { get; set; }
        string DataLocation { get; set; }
        string StorageClass { get; set; }
        string Root { get; set; }
    }

    public class FireStoreSettings : IFireStoreSettings
    { 
        public string ProjectId { get; set; }
        public string DefaultStorageBucket { get; set; }
        public string DataLocation { get; set; }
        public string StorageClass { get; set; }
        public string Root { get; set; }
    }
}
