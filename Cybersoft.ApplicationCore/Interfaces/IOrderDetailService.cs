using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetails>> AddOrUpdateAsync(List<OrderDetails> orderDetail);
        Task<OrderDetails> GetOrderDetailAsync(int orderNumber);
        Task<List<OrderDetails>> GetOrderDetailsForOrderAsync(int orderId);
    }
}
