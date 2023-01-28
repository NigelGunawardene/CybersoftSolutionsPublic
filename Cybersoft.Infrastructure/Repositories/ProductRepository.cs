using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.Infrastructure.Repositories
{
    public class ProductRepository : EfRepository<Products, int>, IProductRepository
    {
        private readonly CyberSoftContext _dbContext;

        public ProductRepository(CyberSoftContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Products> GetProduct(string productName)
        {
            return (Task<Products>)_dbContext.Products.Where(x => x.Name == productName);
        }

        public Task<Products> GetProduct(int productId)
        {
            return (Task<Products>)_dbContext.Products.Where(x => x.Id == productId);
        }
    }
}