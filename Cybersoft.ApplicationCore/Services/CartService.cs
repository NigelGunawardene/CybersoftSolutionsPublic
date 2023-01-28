using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.ApplicationCore.Specifications;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartItemModel> AddOrUpdateAsync(CartItems cartItem)
        {
            ValidateCartItem(cartItem);

            var addedOrUpdatedCartItem = await ProcessCartItemAsync(cartItem);
            return _mapper.Map<CartItemModel>(addedOrUpdatedCartItem);
        }
        private void ValidateCartItem(CartItems cartItem)
        {
            if (string.IsNullOrEmpty(cartItem.ProductName))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(CartItems.ProductName));
            }
            if (cartItem.Price == 0)
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(CartItems.Price));
            }
            if (cartItem.Quantity == 0)
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(CartItems.Quantity));
            }
        }

        private async Task<CartItems> ProcessCartItemAsync(CartItems cartItem)
        {
            var addedOrUpdatedCartItem = await AddOrUpdateCartItemAsync(cartItem); //.ConfigureAwait(false)
            return addedOrUpdatedCartItem;
        }

        private async Task<CartItems> AddOrUpdateCartItemAsync(CartItems cartItem)
        {
            var existingCartItem = await _unitOfWork.Cart.FirstOrDefaultAsync(new CartItemByUserIdAndProductIdSpecification(cartItem.UserId, cartItem.ProductId));
            if (existingCartItem != null)
            {
                existingCartItem.Quantity = existingCartItem.Quantity + cartItem.Quantity; // existingCartItem.Quantity +
                await _unitOfWork.Cart.UpdateAsync(existingCartItem);
                return existingCartItem;
            }
            cartItem = await FormatCartItem(cartItem);
            await _unitOfWork.Cart.AddAsync(cartItem);
            return cartItem;
        }

        private async Task<CartItems> FormatCartItem(CartItems cartItem)
        {
            // get product details(price) from products here in order to avoid API request manipulation
            var actualProductDetails = await _unitOfWork.Products.GetByIdAsync(cartItem.ProductId);
            cartItem.Price = actualProductDetails.Price;
            cartItem.ProductId = actualProductDetails.Id;
            cartItem.Product = actualProductDetails;
            cartItem.ProductName = actualProductDetails.Name;
            return cartItem;
        }

        private void ValidateCartItemName(int cartItemId)
        {
            if (cartItemId == 0)
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(CartItems.Id));
            }
        }

        public async Task DeleteAsync(int cartItemId)
        {
            try
            {
                ValidateCartItemName(cartItemId);
                var doesCartItemExist = await isExistingCartItem(cartItemId);

                if (doesCartItemExist)
                {
                    var existingCartItem = await GetCartItemAsync(cartItemId);
                    await _unitOfWork.Cart.DeleteAsync(existingCartItem).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task<CartItems> GetCartItemAsync(int cartItemId)
        {
            var specification = new CartItemByCartItemIdSpecification(cartItemId);
            return await _unitOfWork.Cart.FirstOrDefaultAsync(specification); //.ConfigureAwait(false)
        }

        public async Task<List<CartItemModel>> GetCartItemsAsync(int userId)
        {
            var specification = new CartItemsByUserIdSpecification(userId);
            var cartItemList = await _unitOfWork.Cart.ListAsync(specification).ConfigureAwait(false);

            var listOfCartItemModels = new List<CartItemModel>();

            cartItemList.ToList().ForEach(item => listOfCartItemModels.Add(_mapper.Map<CartItemModel>(item)));

            return listOfCartItemModels;
        }

        private async Task<bool> isExistingCartItem(int cartItemId)
        {
            var specification = new CartItemByCartItemIdSpecification(cartItemId);
            int cartItemCount = await _unitOfWork.Cart.CountAsync(specification).ConfigureAwait(false);
            if (cartItemCount != 0)
            {
                return cartItemCount == 1;
            }
            return cartItemCount == 0;
        }

    }
}
