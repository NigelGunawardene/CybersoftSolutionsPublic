using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IOrderDetailRepository : IAsyncRepository<OrderDetails, int>
    {
        Task<OrderDetails> GetOrderDetail(int orderNumber);
    }
}
