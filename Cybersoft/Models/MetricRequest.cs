using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.Models
{
    public class MetricRequest <T>
    {
        public string UserName{ get; set; }

        public T Data { get; set; }
    }
}
