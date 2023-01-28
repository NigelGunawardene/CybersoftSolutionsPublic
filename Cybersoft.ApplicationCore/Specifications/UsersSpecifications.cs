using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Specifications
{
    public class PaginatedUsersSpecification : BaseSpecification<Users, int>
    {
        public PaginatedUsersSpecification(int pagesize, int pagenumber)
            : base(null)
        {
            ApplyOrderByDescending(x => x.JoinedDate);
            if (pagenumber < 1)
            {
                pagenumber = 1;
            }

            if (pagesize > 0)
            {
                ApplyPaging((pagenumber - 1) * pagesize, pagesize);
            }
        }
    }

    public class UsersTotalCountSpecification : BaseSpecification<Users, int>
    {
        public UsersTotalCountSpecification()
            : base(null)
        {
            //ApplyOrderByDescending(x => x.JoinedDate);   
        }
    }

    public class UsersTotalCustomerCountSpecification : BaseSpecification<Users, int>
    {
        public UsersTotalCustomerCountSpecification()
            : base(x => x.Role == UserRole.Customer)
        {
            //ApplyOrderByDescending(x => x.JoinedDate);   
        }
    }

    public class UserByEmailAndPasswordSpecification : BaseSpecification<Users, int>
    {
        public UserByEmailAndPasswordSpecification(string email, string password)
            : base(x => x.Email == email && x.Password == password)
        {

        }
    }

    public class UserByEmailSpecification : BaseSpecification<Users, int>
    {
        public UserByEmailSpecification(string email)
            : base(x => x.Email == email && (x.Role == Enums.UserRole.Customer || x.Role == Enums.UserRole.Admin))
        {

        }
    }

    public class UserByPhoneNumberSpecification : BaseSpecification<Users, int>
    {
        public UserByPhoneNumberSpecification(string phoneNumber)
            : base(x => x.PhoneNumber == phoneNumber)
        {

        }
    }
    public class UserByRoleSpecification : BaseSpecification<Users, int>
    {
        public UserByRoleSpecification(UserRole role)
            : base(x => x.Role == role)
        {
            ApplyOrderByDescending(x => x.JoinedDate);
        }
    }

    public class UserByUserIdSpecification : BaseSpecification<Users, int>
    {
        public UserByUserIdSpecification(int userId)
            : base(x => x.Id == userId)
        {

        }
    }

    public class UserByUserNameAndPasswordSpecification : BaseSpecification<Users, int>
    {
        public UserByUserNameAndPasswordSpecification(string userName, string password)
            : base(x => x.UserName == userName && x.Password == password)
        {

        }
    }

    public class UserByUserNameAndRefreshTokenSpecification : BaseSpecification<Users, int>
    {
        public UserByUserNameAndRefreshTokenSpecification(string userName, string refreshToken)
            : base(x => x.UserName == userName && x.RefreshToken == refreshToken)
        {

        }
    }

    public class UserByUserNameSpecification : BaseSpecification<Users, int>
    {
        public UserByUserNameSpecification(string userName)
            : base(x => x.UserName == userName && (x.Role == Enums.UserRole.Customer || x.Role == Enums.UserRole.Admin))
        {

        }
    }
}
