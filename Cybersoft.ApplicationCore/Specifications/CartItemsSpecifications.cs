using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Specifications
{
    public class CartItemByCartItemIdSpecification : BaseSpecification<CartItems, int>
    {
        public CartItemByCartItemIdSpecification(int cartItemId)
            : base(x => x.Id == cartItemId)
        {

        }
    }

    public class CartItemByUserIdAndProductIdSpecification : BaseSpecification<CartItems, int>
    {
        public CartItemByUserIdAndProductIdSpecification(int userId, int productId)
            : base(x => x.UserId == userId && x.ProductId == productId)
        {

        }
    }

    public class CartItemsByUserIdSpecification : BaseSpecification<CartItems, int>
    {
        public CartItemsByUserIdSpecification(int userId)
            : base(x => x.UserId == userId)
        {

        }
    }
}
