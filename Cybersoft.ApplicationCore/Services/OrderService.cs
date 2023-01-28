using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.ApplicationCore.Specifications;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Services
{
    public class OrderService : IOrderService
    {

        //private readonly IAsyncRepository<Orders, int> _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) //, IMapper mapper
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderModel> AddOrUpdateAsync(ShoppingCartOrderModel model)
        {
            Orders completedOrder = await ProcessOrderAsync(model);
            return _mapper.Map<OrderModel>(completedOrder);
        }
        private async Task<Orders> ProcessOrderAsync(ShoppingCartOrderModel model)
        {
            Orders newOrder = await FormatOrderAsync(model);
            await _unitOfWork.Orders.AddAsync(newOrder);
            return newOrder;
        }

        private async Task<Orders> FormatOrderAsync(ShoppingCartOrderModel model)
        {
            Orders newOrder = new Orders();

            newOrder.UserId = model.UserId;
            newOrder.PublicOrderNumber = await GenerateUniquePublicOrderNumberAsync(model.UserId);
            newOrder.IsComplete = false;
            newOrder.IsDeleted = false;
            newOrder.OrderDetails = new List<OrderDetails>();
            foreach (var item in model.OrderedProductDetails)
            {
                OrderDetails newOrderDetail = new OrderDetails();

                var specification = new CartItemByCartItemIdSpecification(item.cartItemId);
                CartItems existingCartItem = await _unitOfWork.Cart.FirstOrDefaultAsync(specification);

                newOrderDetail.ProductId = existingCartItem.ProductId;
                newOrderDetail.Quantity = existingCartItem.Quantity;
                newOrderDetail.Price = existingCartItem.Price;
                newOrderDetail.TotalPrice = (newOrderDetail.Price * newOrderDetail.Quantity);
                newOrder.TotalPrice = newOrder.TotalPrice + newOrderDetail.TotalPrice;
                newOrder.OrderDetails.Add(newOrderDetail);
            }
            return newOrder;
        }


        private async Task<string> GenerateUniquePublicOrderNumberAsync(int userId)
        {
            var specification = new UserByUserIdSpecification(userId);
            Users tempUser = await _unitOfWork.Users.FirstOrDefaultAsync(specification);

            string firstName = tempUser.FirstName.Substring(0, 3).ToUpper();
            string lastName = tempUser.LastName.Substring(0, 3).ToUpper();
            string userName = tempUser.UserName.Substring(0, 3).ToUpper();
            string tempOrderNumber = "000";

            Orders latestOrder = await _unitOfWork.Orders.GetLatestOrderById(userId);
            if (latestOrder != null)
            {
                tempOrderNumber = "00" + latestOrder.Id.ToString();
            }
            var orderNumber = firstName + userName + lastName + tempOrderNumber;
            var doesOrderExist = await checkIfPublicOrderNumberExistsAsync(orderNumber);
            while (doesOrderExist)
            {
                Random rand = new Random();
                int number = rand.Next(0, 20);
                orderNumber = orderNumber + number.ToString();
                doesOrderExist = await checkIfPublicOrderNumberExistsAsync(orderNumber);
            }
            return orderNumber;
        }

        private async Task<bool> checkIfPublicOrderNumberExistsAsync(string orderNumber)
        {
            var checkOrderSpecification = new OrderByPublicOrderNumberSpecification(orderNumber);
            Orders orderCheck = await _unitOfWork.Orders.FirstOrDefaultAsync(checkOrderSpecification);
            if (orderCheck == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ValidateOrderName(int orderNumber)
        {
            if (orderNumber == 0)
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Orders.OrderDetails));
            }
        }

        public async Task DeleteAsync(int orderNumber)
        {
            ValidateOrderName(orderNumber);
            var doesUserExist = await isExistingOrder(orderNumber);

            if (!doesUserExist)
            {
                var existingUser = await GetOrderInternallyAsync(orderNumber);
                await _unitOfWork.Orders.DeleteAsync(existingUser).ConfigureAwait(false);
            }
        }

        public async Task<OrderModel> GetOrderAsync(int orderNumber)
        {
            var specification = new OrderByOrderIdSpecification(orderNumber);
            return _mapper.Map<OrderModel>(await _unitOfWork.Orders.FirstOrDefaultAsync(specification)); //.ConfigureAwait(false)
        }

        private async Task<Orders> GetOrderInternallyAsync(int orderNumber)
        {
            var specification = new OrderByOrderIdSpecification(orderNumber);
            return await _unitOfWork.Orders.FirstOrDefaultAsync(specification); //.ConfigureAwait(false)
        }

        private async Task<bool> isExistingOrder(int orderNumber)
        {
            var specification = new OrderByOrderIdSpecification(orderNumber);
            int orderCount = await _unitOfWork.Orders.CountAsync(specification).ConfigureAwait(false);
            if (orderCount != 0)
            {
                return orderCount == 1;
            }
            return orderCount == 0;
        }

        public async Task<List<OrderModel>> GetAllOrdersForUserAsync(int userId)
        {
            var specification = new OrderByUserIdSpecification(userId);
            var usersOrders = await _unitOfWork.Orders.ListAsync(specification).ConfigureAwait(false);

            List<OrderModel> orderModelsForUser = new();
            usersOrders.ToList().ForEach(item => orderModelsForUser.Add(_mapper.Map<OrderModel>(item)));
            return orderModelsForUser;
        }



        public async Task<List<OrderModel>> GetAllActiveOrdersAsync() // Active is when complete = 0.
        {
            var specification = new OrderByIsCompleteSpecification(false);
            var activeOrders = await _unitOfWork.Orders.ListAsync(specification).ConfigureAwait(false);

            List<OrderModel> activeOrdersForUser = new();
            activeOrders.ToList().ForEach(item => activeOrdersForUser.Add(_mapper.Map<OrderModel>(item)));
            return activeOrdersForUser;
        }

        public async Task<List<OrderModel>> GetAllOrdersAsync()
        {
            var allOrders = await _unitOfWork.Orders.ListAllAsync().ConfigureAwait(false);
            List<OrderModel> allOrdersModel = new();
            allOrders.ToList().ForEach(item => allOrdersModel.Add(_mapper.Map<OrderModel>(item)));
            return allOrdersModel;
        }

        public async Task<bool> CompleteOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.FirstOrDefaultAsync(new OrderByOrderIdSpecification(orderId));
            if (order != null)
            {
                order.IsComplete = true;
                await _unitOfWork.Orders.UpdateAsync(order);
                return true;
            }
            return false;
        }


        public async Task<PaginatedList<Orders>> GetPaginatedOrdersAsync(string orderstatus, int pagesize, int pagenumber, string includes)
        {
            IEnumerable<Orders> orders;
            var status = false;
            var numberOfOpenOrders = 0;
            switch (orderstatus)
            {
                case "active":
                    status = false;
                    orders = await _unitOfWork.Orders.GetPaginatedItemsWithFilterAsync(filter: x => x.IsComplete == status, orderBy: x => x.OrderByDescending(x => x.OrderDate), first: pagesize, offset: pagenumber, includeProperties: includes);
                    numberOfOpenOrders = await _unitOfWork.Orders.CountAsync(new GetOrdersCountSpecification(status)).ConfigureAwait(false);
                    break;
                case "complete":
                    status = true;
                    orders = await _unitOfWork.Orders.GetPaginatedItemsWithFilterAsync(filter: x => x.IsComplete == status, orderBy: x => x.OrderByDescending(x => x.OrderDate), first: pagesize, offset: pagenumber);
                    numberOfOpenOrders = await _unitOfWork.Orders.CountAsync(new GetOrdersCountSpecification(status)).ConfigureAwait(false);
                    break;
                case "all":
                    orders = await _unitOfWork.Orders.GetPaginatedItemsWithFilterAsync(orderBy: x => x.OrderByDescending(x => x.OrderDate), first: pagesize, offset: pagenumber);
                    numberOfOpenOrders = await _unitOfWork.Orders.CountAsync(new GetOrdersCountSpecification(status)).ConfigureAwait(false) + await _unitOfWork.Orders.CountAsync(new GetOrdersCountSpecification(!status)).ConfigureAwait(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status));
            }
            //var orderStatus = DetermineOrderStatus(orderstatus);
            var parameters = new PaginationParams(pagesize, pagenumber);
            List<Orders> listOfOrders = orders.ToList();
            var paginatedResponse = PaginatedList<Orders>.ToCustomPagedList(orders.AsQueryable(), numberOfOpenOrders, pagenumber, pagesize);
            return paginatedResponse;
        }

        //public bool DetermineOrderStatus(string orderstatus)
        //{
        //    if (orderstatus.Equals("all"))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //private OrderModel MapOrderToOrderModel(Orders item)
        //{
        //    OrderModel orderModel = new()
        //    {
        //        Id = item.Id,
        //        UserId = item.UserId,
        //        User = item.User,
        //        TotalPrice = item.TotalPrice,
        //        PublicOrderNumber = item.PublicOrderNumber,
        //        Message = item.Message,
        //        OrderDate = DateOnly.FromDateTime(item.OrderDate).ToString(),
        //        IsComplete = item.IsComplete,
        //        IsDeleted = item.IsDeleted,
        //        OrderDetails = item.OrderDetails,
        //    };
        //    return orderModel;
        //}
    }
}
