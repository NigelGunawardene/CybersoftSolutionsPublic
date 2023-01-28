using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CyberSoftContext _context;
        public IUserRepository Users { get; private set; }
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }
        public ICartRepository Cart { get; private set; }

        public UnitOfWork(CyberSoftContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Products = new ProductRepository(_context);
            Orders = new OrderRepository(_context);
            OrderDetails = new OrderDetailRepository(_context);
            Cart = new CartRepository(_context);
            //Projects = new ProjectRepository(_context);
        }


        // add new tables here
        //public IProjectRepository Projects { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
