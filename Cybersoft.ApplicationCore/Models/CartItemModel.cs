using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public UserModel User { get; set; }
        public int ProductId { get; set; }
        //public Products Product { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
