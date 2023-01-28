using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cybersoft.ApplicationCore.Models
{
    public class ShoppingCartOrderModel
    {
        public int UserId { get; set; }
        //[JsonIgnore]
        public virtual ICollection<OrderProductDetailModel> OrderedProductDetails { get; set; }
        //public virtual ICollection<Products> OrderProducts { get; set; }
    }
}
