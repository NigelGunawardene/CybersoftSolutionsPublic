using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.Infrastructure.Repositories
{
    public class OrderDetailRepository : EfRepository<OrderDetails, int>, IOrderDetailRepository
    {
        private readonly CyberSoftContext _dbContext;

        public OrderDetailRepository(CyberSoftContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<OrderDetails> GetOrderDetail(int orderDetailId)
        {
            return (Task<OrderDetails>)_dbContext.OrderDetails.Where(x => x.Id == orderDetailId);
        }
    }
}
