using Cybersoft.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cybersoft.ApplicationCore.Entities
{
    public class CartItems : BaseEntity<int>
    {
        public int UserId { get; set; }
        public Users User { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }
        //public virtual ICollection<Orders> Orders { get; set; }
    }
}
