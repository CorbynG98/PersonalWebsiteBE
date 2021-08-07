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
    }

    public class FireStoreSettings : IFireStoreSettings
    { 
        public string ProjectId { get; set; }
    }
}
