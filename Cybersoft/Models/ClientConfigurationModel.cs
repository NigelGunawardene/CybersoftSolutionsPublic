using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cybersoft.Models
{
    public class ClientConfigurationModel
    {
        public string EnvironmentName { get; internal set; }
        public string EnableHostAuthentication { get; set; }
        public string GoogleTagManagerId { get; set; }
        public string ProfileImagePath { get; set; }
        public AuthConfigurationModel AuthConfiguration { get; set; }
        public string PageSize { get; set; }
    }
}
