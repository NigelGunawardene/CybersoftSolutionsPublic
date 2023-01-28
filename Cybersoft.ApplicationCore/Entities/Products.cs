using Cybersoft.ApplicationCore.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Cybersoft.ApplicationCore.Entities
{
    public class Products : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public virtual ICollection<OrderDetails> ProductOrderDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<CartItems> CartItems { get; set; }
    }
}
