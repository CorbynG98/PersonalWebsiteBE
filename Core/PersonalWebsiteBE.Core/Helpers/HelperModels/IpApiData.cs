using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Helpers.HelperModels
{
    public class IpApiData
    {
        public string Continent { get; set; }
        public string ContinentCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Timezone { get; set; }
        public int Offset { get; set; }
        public string ISP { get; set; }
        public string As { get; set; }
        public string AsName { get; set; }
        public bool Mobile { get; set; }
    }
}
