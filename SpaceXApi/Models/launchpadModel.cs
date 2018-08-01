using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXApi.Models
{
    public class LaunchpadModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }

    }

    public class Endpoint
    {
        public string DataLoc { get; set; }
    }

}
