using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Specifications
{
    public class GetOrdersCountSpecification : BaseSpecification<Orders, int>
    {
        public GetOrdersCountSpecification(bool orderstatus)
            : base(o => o.IsComplete == orderstatus)
        {
            //ApplyOrderByDescending(x => x.JoinedDate);   
        }
    }

    public class OrderByIsCompleteSpecification : BaseSpecification<Orders, int>
    {
        public OrderByIsCompleteSpecification(bool isComplete)
            : base(x => x.IsComplete == isComplete)
        {
            ApplyOrderByDescending(x => x.OrderDate);
        }
    }

    public class OrderByOrderIdSpecification : BaseSpecification<Orders, int>
    {
        public OrderByOrderIdSpecification(int orderNumber)
            : base(x => x.Id == orderNumber)
        {

        }
    }

    public class OrderByPublicOrderNumberSpecification : BaseSpecification<Orders, int>
    {
        public OrderByPublicOrderNumberSpecification(string publicOrderNumber)
            : base(x => x.PublicOrderNumber == publicOrderNumber)
        {

        }
    }

    public class OrderByUserIdSpecification : BaseSpecification<Orders, int>
    {
        public OrderByUserIdSpecification(int userId)
            : base(x => x.UserId == userId)
        {
            //Includes(x= > x);
            AddInclude(x => x.OrderDetails);
            AddInclude($"{nameof(Orders.OrderDetails)}.{nameof(OrderDetails.Product)}");
            ApplyOrderByDescending(x => x.OrderDate);

            //AddInclude(x => x.OrderDetails);
            //AddInclude("OrderDetails.Product");
        }
    }
}
