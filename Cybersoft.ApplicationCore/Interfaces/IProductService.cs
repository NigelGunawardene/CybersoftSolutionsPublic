using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IProductService
    {
        Task<Products> UpsertProductAsync(Products product);
        Task<Products> GetProductAsync(int productId);
        Task<List<Products>> GetProductsAsync();
        Task<PaginatedList<Products>> GetPaginatedProductsAsync(int pagesize, int pagenumber);
        //Task<PagedCollection<Products>> GetPaginatedProductsAsync(int pagesize, int pagenumber);
        PaginatedList<Products> GetPaginatedProducts(int pagesize, int pagenumber);
        Task DeleteProductAsync(int id);
    }
}
