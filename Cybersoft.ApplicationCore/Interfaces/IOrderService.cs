using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> AddOrUpdateAsync(ShoppingCartOrderModel model);
        Task<OrderModel> GetOrderAsync(int orderNumber);
        Task<List<OrderModel>> GetAllOrdersForUserAsync(int userId);
        Task<List<OrderModel>> GetAllActiveOrdersAsync();
        Task<List<OrderModel>> GetAllOrdersAsync();
        Task<Boolean> CompleteOrderAsync(int orderId);
        Task<PaginatedList<Orders>> GetPaginatedOrdersAsync(string orderstatus, int pagesize, int pagenumber, string includes);
    }
}
