using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Specifications
{
    public class OrderDetailByOrderDetailIdSpecification : BaseSpecification<OrderDetails, int>
    {
        public OrderDetailByOrderDetailIdSpecification(int orderDetailId)
            : base(x => x.Id == orderDetailId)
        {

        }
    }

    public class OrderDetailByOrderIdSpecification : BaseSpecification<OrderDetails, int>
    {
        public OrderDetailByOrderIdSpecification(int orderId)
            : base(x => x.OrderId == orderId)
        {
            AddInclude(x => x.Product);
        }
    }
}
