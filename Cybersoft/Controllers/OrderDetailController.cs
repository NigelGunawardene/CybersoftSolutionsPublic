using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.ApplicationCore.Specifications;
using Cybersoft.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cybersoft.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : CommonApiController
    {
        private readonly ILogger<OrderDetailController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(ILogger<OrderDetailController> logger, IOrderService orderService, IUnitOfWork unitOfWork, IOrderDetailService orderDetailService)
        {
            _logger = logger;
            _orderService = orderService;
            _unitOfWork = unitOfWork;
            _orderDetailService = orderDetailService;
        }

        [Route("{orderId}")]
        [HttpGet]
        public async Task<List<OrderDetails>> GetOrderDetailsForOrder(int orderId)
        {
            //var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification("Test User Two"));
            var orderDetailsforOrderId = await _orderDetailService.GetOrderDetailsForOrderAsync(orderId).ConfigureAwait(false);
            return orderDetailsforOrderId;
        }

        [Route("orderDetail/{orderDetailNumber}")]
        [HttpGet]
        public async Task<OrderDetails> GetOrderDetail(int orderDetailNumber)
        {
            //var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification("Test User Two"));
            var user = await _orderDetailService.GetOrderDetailAsync(orderDetailNumber).ConfigureAwait(false);
            return user;
        }

        //[Route("user")]
        //[HttpGet]
        //public async Task<List<OrderDetails>> GetAllOrdersForUser()
        //{
        //    var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification(UserName));
        //    var userId = user.Id;

        //    var ordersForUser = await _orderDetailService.GetAllOrdersForUser(userId).ConfigureAwait(false);
        //    return ordersForUser;
        //}

        ////[Authorize(Roles = "Admin, SuperAdmin")]
        //[Route("active")]
        //[HttpGet]
        //public async Task<List<OrderDetails>> GetAllActiveOrders()
        //{
        //    var cartItems = await _orderDetailService.GetAllActiveOrders().ConfigureAwait(false);
        //    return cartItems;
        //}

        ////[Authorize(Roles = "Admin, SuperAdmin")]
        //[Route("all")]
        //[HttpGet]
        //public async Task<List<OrderDetails>> GetAllOrders()
        //{
        //    var cartItems = await _orderDetailService.GetAllOrders().ConfigureAwait(false);
        //    return cartItems;
        //}

    }
}
