using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface ICartRepository : IAsyncRepository<CartItems, int>
    {
        Task<CartItems> GetCartItem(int cartItemId);
    }
}
