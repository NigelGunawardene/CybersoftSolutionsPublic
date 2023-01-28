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

namespace Cybersoft.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartController : CommonApiController
{
    private readonly ILogger<CommonApiController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartService _cartService;
    //private readonly IUserService _userService;

    public CartController(ILogger<CommonApiController> logger, ICartService cartService, IUnitOfWork unitOfWork) //, IUserService userService
    {
        _logger = logger;
        _cartService = cartService;
        _unitOfWork = unitOfWork;
        //_userService = userService;
    }

    [HttpPost]
    public async Task<CartItemModel> AddCartItems([FromBody] CartItems cartItemsModel) //[FromBody] user //HttpRequest req
    {
        //cartItemsModel.AddedDate = DateTime.Now;
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification(UserName));
        cartItemsModel.UserId = user.Id;
        var enteredcartItem = await _cartService.AddOrUpdateAsync(cartItemsModel);
        return enteredcartItem;
    }

    [HttpGet]
    public async Task<List<CartItemModel>> GetCartItems() //int userId
    {
        //var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification("Test User Two"));
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification(UserName));
        var userId = user.Id;

        var cartItems = await _cartService.GetCartItemsAsync(userId).ConfigureAwait(false);
        return cartItems;
    }

    //[Route("item/{cartItemId}")]
    //[HttpGet]
    //public async Task<CartItems> GetProduct(int cartItemId)
    //{
    //    //var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification("Test User Two"));
    //    var cartItems = await _cartService.GetCartItemAsync(cartItemId).ConfigureAwait(false);
    //    return cartItems;
    //}


    [Route("removecartitem/{cartItemId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteCartItem(int cartItemId) //[FromBody] user //HttpRequest req
    {
        //cartItemsModel.AddedDate = DateTime.Now;
        await _cartService.DeleteAsync(cartItemId);
        return Ok();
    }

    //[Route("me")]
    //[HttpGet]
    //public async Task<IActionResult> GetCurrentUser()
    //{
    //    var user = await _cartItemService.GetCurrentUser(UserName).ConfigureAwait(false);
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(user);
    //}

    //public int getCurrentUserId()
    //{
    //    var user = _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification(UserName));
    //    return user.Id;
    //}
}
