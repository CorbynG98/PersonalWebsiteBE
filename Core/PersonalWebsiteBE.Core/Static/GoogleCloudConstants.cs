using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Static
{
    public static class GoogleCloudConstants
    {
        public static readonly string EmailContentLogBucketName = "ctg_email_content"; // Email log content can get zipped and stored here, rather than on the log itself. Save space and money
        public static readonly string PublicBucketName = "public_images_ctg"; // Probably only used on website tbh
    }
}
