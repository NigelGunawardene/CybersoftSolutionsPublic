using Cybersoft.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.ApplicationCore.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
