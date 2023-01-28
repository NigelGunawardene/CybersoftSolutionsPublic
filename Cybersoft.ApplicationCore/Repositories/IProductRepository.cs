using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IProductRepository : IAsyncRepository<Products, int>
    {
        Task<Products> GetProduct(int productId);
        //IEnumerable<Products> GetPaginatedProducts(PaginationParams ownerParameters);
    }
}
