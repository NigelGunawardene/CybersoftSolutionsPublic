using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.ApplicationCore.Interfaces;
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
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;

namespace Cybersoft.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : CommonApiController
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly IMemoryCache _memoryCache;
        private string productListCacheKey = "products";
        private int productsCacheLifetimeInDays = 60;
        private static readonly SemaphoreSlim productsCacheSemaphore = new SemaphoreSlim(1, 1);

        public ProductController(ILogger<ProductController> logger, IProductService productService, IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _logger = logger;
            _productService = productService;
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> UpsertProduct([FromBody] Products productModel)
        {
            productModel.AddedDate = DateTime.Now;
            var enteredProduct = await _productService.UpsertProductAsync(productModel);


            _memoryCache.Remove(productListCacheKey);
            return Ok(enteredProduct);
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpDelete]
        [Route("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productService.DeleteProductAsync(productId);
            _memoryCache.Remove(productListCacheKey);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                await productsCacheSemaphore.WaitAsync();
                if (_memoryCache.TryGetValue(productListCacheKey, out IEnumerable<Products> productsCache))
                {
                    return Ok((List<Products>)productsCache);
                }
                else
                {
                    var products = await _productService.GetProductsAsync().ConfigureAwait(false);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromDays(productsCacheLifetimeInDays))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(productsCacheLifetimeInDays))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                    _memoryCache.Set(productListCacheKey, products, cacheEntryOptions);
                    return Ok(products);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
            finally
            {
                productsCacheSemaphore.Release();
            }
        }

        [Route("{productId}")]
        [HttpGet]
        public async Task<IActionResult> GetProduct(int productId)
        {
            //var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification("Test User Two"));
            var products = await _productService.GetProductAsync(productId).ConfigureAwait(false);
            return Ok(products);
        }

        [AllowAnonymous]
        [Route("paginatedproducts")]
        [HttpGet]
        public IActionResult GetPaginatedProducts(int pagesize, int pagenumber)
        {
            var products = _productService.GetPaginatedProducts(pagesize, pagenumber);
            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(products);
        }

        [AllowAnonymous]
        [Route("paginatedproductsasync")]
        [HttpGet]
        public async Task<IActionResult> GetPaginatedProductsAsync(int pagesize, int pagenumber)
        {
            var products = await _productService.GetPaginatedProductsAsync(pagesize, pagenumber).ConfigureAwait(false);
            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(products);
        }

        [AllowAnonymous]
        [Route("invalidatecache")]
        [HttpGet]
        public IActionResult InvalidateCache()
        {
            _memoryCache.Remove(productListCacheKey);
            return NoContent();
        }

        //[Route("me")]
        //[HttpGet]
        //public async Task<IActionResult> GetCurrentUser()
        //{
        //    var user = await _productService.GetCurrentUser(UserName).ConfigureAwait(false);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}


    }
}
