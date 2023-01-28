using Cybersoft.ApplicationCore.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Cybersoft.ApplicationCore.Entities
{
    public class Orders : BaseEntity<int>
    {
        public int UserId { get; set; }
        [IgnoreDataMember]
        public Users User { get; set; }
        public double TotalPrice { get; set; }
        public string PublicOrderNumber { get; set; } // this is only to show the customer. Does not impact code at all
        public string Message { get; set; }
        public DateTime OrderDate { get; set; }
        public Boolean IsComplete { get; set; }
        public Boolean IsDeleted { get; set; }
        //[JsonIgnore]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        //public virtual ICollection<Products> OrderProducts { get; set; }
    }
}
