using Cybersoft.ApplicationCore.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Cybersoft.ApplicationCore.Entities
{
    public class Users : BaseEntity<int>
    {
        public string UserName { get; set; }
        //[IgnoreDataMember]
        //[JsonIgnore]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public DateTime JoinedDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenAddedTime { get; set; }
        //[JsonIgnore]
        public virtual ICollection<Orders> OrderList { get; set; }
        //[JsonIgnore]
        public virtual ICollection<CartItems> Cart { get; set; }
    }
}
