using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Helpers;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Cybersoft.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : CommonApiController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IConfiguration _configuration;


        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IUnitOfWork unitOfWork, ICartService cartService, IConfiguration configuration)
        {
            _logger = logger;
            _orderService = orderService;
            _unitOfWork = unitOfWork;
            _cartService = cartService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] int[] listOfCartItemIds)
        {
            if (listOfCartItemIds.Length == 0)
            {
                return BadRequest();
            }

            ShoppingCartOrderModel orderModel = new ShoppingCartOrderModel();
            List<OrderProductDetailModel> listOfIdsForPlacingOrder = new List<OrderProductDetailModel>();
            listOfCartItemIds.ToList().ForEach(item => listOfIdsForPlacingOrder.Add(new OrderProductDetailModel { cartItemId = item }));

            orderModel.OrderedProductDetails = listOfIdsForPlacingOrder;

            var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification(UserName));
            orderModel.UserId = user.Id;

            var enteredOrder = await _orderService.AddOrUpdateAsync(orderModel);

            foreach (var item in orderModel.OrderedProductDetails)
            {
                await _cartService.DeleteAsync(item.cartItemId);
            }

            var specification = new OrderDetailByOrderIdSpecification(enteredOrder.Id);
            var currentOrderDetails = (List<OrderDetails>)await _unitOfWork.OrderDetails.ListAsync(specification);

            Task sendEmail = Task.Run(() => EmailTrigger(enteredOrder, currentOrderDetails));
            Task sendTelegramNotification = Task.Run(() => TelegramBotTrigger(enteredOrder, currentOrderDetails));
            return Ok(enteredOrder);
        }

        [Route("{orderNumber}")]
        [HttpGet]
        public async Task<OrderModel> GetOrder(int orderNumber)
        {
            //var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification("Test User Two"));
            var user = await _orderService.GetOrderAsync(orderNumber).ConfigureAwait(false);
            return user;
        }

        [Route("user")]
        [HttpGet]
        public async Task<List<OrderModel>> GetAllOrdersForUser()
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification(UserName));
            var userId = user.Id;

            var ordersForUser = await _orderService.GetAllOrdersForUserAsync(userId).ConfigureAwait(false);
            return ordersForUser;
        }

        //[Authorize(Roles = "Admin, SuperAdmin")]
        [Route("active")]
        //[AllowAnonymous]
        [HttpGet]
        public async Task<List<OrderModel>> GetAllActiveOrders()
        {
            var cartItems = await _orderService.GetAllActiveOrdersAsync().ConfigureAwait(false);
            return cartItems;
        }

        //[Authorize(Roles = "Admin, SuperAdmin")]
        [Route("all")]
        //[AllowAnonymous]
        [HttpGet]
        public async Task<List<OrderModel>> GetAllOrders()
        {
            var cartItems = await _orderService.GetAllOrdersAsync().ConfigureAwait(false);
            return cartItems;
        }

        //[Authorize(Roles = "Admin, SuperAdmin")]
        //[AllowAnonymous]
        [Route("paginated")] //[Route("all/{accountType}/{pagesize}/{pagenumber}")]
        [HttpGet]
        public async Task<IActionResult> GetPaginatedOrdersAsync(string orderstatus, int pagesize, int pagenumber)
        {
            var orders = await _orderService.GetPaginatedOrdersAsync(orderstatus.ToLower(), pagesize, pagenumber, "OrderDetails,OrderDetails.Product").ConfigureAwait(false);
            var metadata = new
            {
                orders.TotalCount,
                orders.PageSize,
                orders.CurrentPage,
                orders.TotalPages,
                orders.HasNext,
                orders.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(orders);
        }


        private void EmailTrigger(OrderModel order, List<OrderDetails> currentOrderDetails)
        {
            EmailSender emailSender = new EmailSender(_configuration);
            Task sendEmail = Task.Run(() => emailSender.sendOrderEmail(order, currentOrderDetails));
            Thread.Yield();
        }

        private async void TelegramBotTrigger(OrderModel order, List<OrderDetails> currentOrderDetails)
        {
            var messageBody = "You have a new order: " + order.PublicOrderNumber + " from username: " + order.User.UserName + "\n" +
                "Fullname: " + order.User.FullName + "\n" +
                "Email: " + order.User.Email + "\n" +
                "Contact number: " + order.User.PhoneNumber + "\n" +
                "Order details: " + "\n";
            foreach (var detail in currentOrderDetails)
            {
                messageBody = messageBody + detail.Product.Name + " : " + detail.Product.Price + " x " + detail.Quantity.ToString() + " = " + detail.TotalPrice + "\n";
            }
            messageBody = messageBody + "Total price : " + order.TotalPrice;

            try
            {
                var bot = new TelegramBotClient(_configuration.GetValue<string>("TelegramSender:Secret"));
                await bot.SendTextMessageAsync(_configuration.GetValue<string>("TelegramSender:DestinationId"), messageBody);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Thread.Yield();
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("complete/{orderId}")]
        [HttpGet]
        public async Task<bool> CompleteOrder(int orderId)
        {
            return await _orderService.CompleteOrderAsync(orderId);
        }
    }
}
