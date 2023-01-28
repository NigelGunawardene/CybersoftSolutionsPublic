using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Services
{
    public class OrderDetailService : IOrderDetailService
    {

        //private readonly IAsyncRepository<Orders, int> _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private const string DomainName = "";

        public OrderDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderDetails>> AddOrUpdateAsync(List<OrderDetails> orderDetails)
        {
            //ValidateUserName(order.UserName);

            //if (!string.IsNullOrEmpty(order.ManagerName))
            //{
            //    await ProcessManagerAsync(order.ManagerName);
            //}
            //await AddManagerIdAsync(order);
            foreach (var orderDetail in orderDetails) {
                await ProcessOrderAsync(orderDetail);
            }
            return orderDetails;
        }

        private async Task<OrderDetails> ProcessOrderAsync(OrderDetails orderDetail)
        {
            await AddOrderAsync(orderDetail); //.ConfigureAwait(false)
            return orderDetail;
        }

        private async Task<OrderDetails> AddOrderAsync(OrderDetails orderDetail)
        {
            FormatOrder(orderDetail);
            var existingorderDetail = await _unitOfWork.OrderDetails.FirstOrDefaultAsync(new OrderDetailByOrderDetailIdSpecification(orderDetail.Id));
            if (existingorderDetail != null)
            {
                existingorderDetail.Quantity = orderDetail.Quantity;
                await _unitOfWork.OrderDetails.UpdateAsync(existingorderDetail);
                return existingorderDetail;
            }
            await _unitOfWork.OrderDetails.AddAsync(orderDetail);
            //_unitOfWork.Complete();
            return orderDetail;
        }

        private void FormatOrder(OrderDetails order)
        {
            //change order details here
        }

        private void ValidateOrderNumber(int orderNumber)
        {
            if (orderNumber == 0)
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(OrderDetails.OrderId));
            }
        }

        public async Task DeleteAsync(int orderNumber)
        {
            ValidateOrderNumber(orderNumber);
            var doesUserExist = await isExistingOrder(orderNumber);

            if (!doesUserExist)
            {
                var existingUser = await GetOrderDetailAsync(orderNumber);
                await _unitOfWork.OrderDetails.DeleteAsync(existingUser).ConfigureAwait(false);
            }
        }

        public async Task<OrderDetails> GetOrderDetailAsync(int orderDetailId)
        {
            var specification = new OrderDetailByOrderDetailIdSpecification(orderDetailId);
            return await _unitOfWork.OrderDetails.FirstOrDefaultAsync(specification); //.ConfigureAwait(false)
        }

        public async Task<List<OrderDetails>> GetOrderDetailsForOrderAsync(int orderId)
        {
            var specification = new OrderDetailByOrderIdSpecification(orderId);
            return (List<OrderDetails>)await _unitOfWork.OrderDetails.ListAsync(specification); //.ConfigureAwait(false)
        }

        private async Task<bool> isExistingOrder(int orderNumber)
        {
            var specification = new OrderDetailByOrderIdSpecification(orderNumber);
            int orderCount = await _unitOfWork.OrderDetails.CountAsync(specification).ConfigureAwait(false);
            if (orderCount != 0)
            {
                return orderCount == 1;
            }
            return orderCount == 0;
        }

    }
}
