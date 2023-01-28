using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.Infrastructure.Repositories
{
    public class OrderRepository : EfRepository<Orders, int>, IOrderRepository
    {
        private readonly CyberSoftContext _dbContext;

        public OrderRepository(CyberSoftContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Orders> GetOrder(int orderNumber)
        {
            return (Task<Orders>)_dbContext.Orders.Where(x => x.Id == orderNumber);
        }

        public async Task<Orders> GetLatestOrderById(int userId)
        {
            var latestOrderByUser = await _dbContext.Orders.OrderByDescending(p => p.OrderDate).FirstOrDefaultAsync();
            return latestOrderByUser;
        }

    }
}