using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Services
{
    public class ProductService : IProductService
    {
        private const string imageExtension = ".png";
        private const string assetsFolder = "assets/";
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Products> UpsertProductAsync(Products product)
        {
            ValidateProduct(product);
            await ProcessProductAsync(product);
            return product;
        }

        private async Task<Products> ProcessProductAsync(Products product)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(product.Id);
            if (existingProduct == null) await AddProductAsync(product);
            else
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.ImageUrl = product.ImageUrl;
                await _unitOfWork.Products.UpdateAsync(existingProduct);
            }
            return existingProduct;
        }

        private async Task<Products> AddProductAsync(Products product)
        {
            FormatProduct(product);
            await _unitOfWork.Products.AddAsync(product);
            //_unitOfWork.Complete();
            return product;
        }

        private void FormatProduct(Products product)
        {
            //change product details here
        }

        private void ValidateProduct(Products product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Products.Name));
            }
            if (product.Price == 0)
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Products.Price));
            }
            if (string.IsNullOrEmpty(product.Description))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Products.Description));
            }
            var imageUrlConstuct = assetsFolder + RemoveWhitespace(product.Name.ToLower()) + imageExtension;
            imageUrlConstuct = imageUrlConstuct.Replace("+", "");
            product.ImageUrl = imageUrlConstuct;
        }

        private void ValidateProductName(int productId)
        {
            if (productId == 0)
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Products.Name));
            }
        }

        public async Task DeleteAsync(int productId)
        {
            ValidateProductName(productId);
            var doesProductExist = await isExistingProduct(productId);

            if (!doesProductExist)
            {
                var existingProduct = await GetProductAsync(productId);
                await _unitOfWork.Products.DeleteAsync(existingProduct).ConfigureAwait(false);
            }
        }

        public async Task<Products> GetProductAsync(int productId)
        {
            var specification = new ProductByProductIdSpecification(productId);
            return await _unitOfWork.Products.FirstOrDefaultAsync(specification); //.ConfigureAwait(false)
        }

        public async Task<List<Products>> GetProductsAsync()
        {
            var specification = new ProductByDeletedStatusSpecification();
            var productList = await _unitOfWork.Products.ListAsync(specification).ConfigureAwait(false);
            return (List<Products>)productList;
        }

        private async Task<bool> isExistingProduct(string productName)
        {
            var specification = new ProductByProductNameSpecification(productName);
            int productCount = await _unitOfWork.Products.CountAsync(specification).ConfigureAwait(false);
            if (productCount != 0)
            {
                return productCount == 1;
            }
            return productCount == 0;
        }

        private async Task<bool> isExistingProduct(int productId)
        {
            var specification = new ProductByProductIdSpecification(productId);
            int productCount = await _unitOfWork.Products.CountAsync(specification).ConfigureAwait(false);
            if (productCount != 0)
            {
                return productCount == 1;
            }
            return productCount == 0;
        }

        public PaginatedList<Products> GetPaginatedProducts(int pagesize, int pagenumber)
        {
            var products = _unitOfWork.Products.GetPaginatedItems(new PaginationParams(pagesize, pagenumber));
            return PaginatedList<Products>.ToPagedList(products.AsQueryable(), pagenumber, pagesize);
        }

        // ASYNC METHOD TO GET PAGINATED PRODUCTS
        public async Task<PaginatedList<Products>> GetPaginatedProductsAsync(int pagesize, int pagenumber)
        {
            var products = await _unitOfWork.Products.GetPaginatedItemsWithOrderByDescendingIdAsync(new PaginationParams(pagesize, pagenumber));

            var totalProductsSpecification = new ProductsTotalCountSpecification();
            var totalItems = await _unitOfWork.Products.CountAsync(totalProductsSpecification).ConfigureAwait(false);
            return PaginatedList<Products>.ToCustomPagedList(products.AsQueryable(), totalItems, pagenumber, pagesize);
        }

        private static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public async Task DeleteProductAsync(int id)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
            if (existingProduct != null)
            {
                existingProduct.IsDeleted = true;
                await _unitOfWork.Products.UpdateAsync(existingProduct);
            }
        }
    }
}
