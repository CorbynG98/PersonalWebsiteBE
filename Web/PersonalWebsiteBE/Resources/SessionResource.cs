using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Models.Auth
{
    public class SessionResource
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool CurrentSession { get; set; }
        public string IpAddress { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public string RegionName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
    }
}
