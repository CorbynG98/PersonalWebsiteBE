using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Settings
{
    public interface ISendGridSettings
    {
        string ApiKey { get; set; }
    }

    public class SendGridSettings : ISendGridSettings
    {
        public string ApiKey { get; set; }
    }
}
