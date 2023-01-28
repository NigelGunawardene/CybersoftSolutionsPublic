using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IOrderRepository : IAsyncRepository<Orders, int>
    {
        Task<Orders> GetOrder(int orderNumber);
        Task<Orders> GetLatestOrderById(int userId);
    }
}
