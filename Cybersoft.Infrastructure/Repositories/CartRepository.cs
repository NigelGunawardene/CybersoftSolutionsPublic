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
    public class CartRepository : EfRepository<CartItems, int>, ICartRepository
    {
        private readonly CyberSoftContext _dbContext;

        public CartRepository(CyberSoftContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<CartItems> GetCartItem(int cartItemId)
        {
            return (Task<CartItems>)_dbContext.Cart.Where(x => x.Id == cartItemId);
        }
    }
}