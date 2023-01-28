using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface ICartService
    {
        Task<CartItemModel> AddOrUpdateAsync(CartItems product);
        //Task<CartItems> GetCartItemAsync(int cartItemId);
        Task<List<CartItemModel>> GetCartItemsAsync(int UserId);
        Task DeleteAsync(int cartItemId);
    }
}
