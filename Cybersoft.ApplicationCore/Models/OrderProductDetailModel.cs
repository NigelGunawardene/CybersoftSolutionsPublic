using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cybersoft.ApplicationCore.Models
{
    public class OrderProductDetailModel
    {
        public int cartItemId { get; set; }
    }
}
