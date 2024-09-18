using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

namespace DotnetCoding.Infrastructure
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _dbContext;
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork, DbContextClass dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts()
        {
            var productDetailsList = await _unitOfWork.Products.GetAll();
            return productDetailsList;
        }
        public async Task<IEnumerable<ApprovalRequest>> GetApprovalQueue()
        {
            return await _unitOfWork.ApprovalQueues.GetApprovalQueue();
        }
        public async Task<IEnumerable<ProductDetails>> GetActiveProducts(string name = null, decimal? minPrice = null, decimal? maxPrice = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = (IQueryable<ProductDetails>)_dbContext.Products.Where(p => p.Active).OrderByDescending(p => p.CreatedAt);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.ProductName.Contains(name));

            if (minPrice != null && minPrice.HasValue)
                query = query.Where(p => p.ProductPrice >= minPrice.Value);

            if (maxPrice != null && maxPrice.HasValue)
                query = query.Where(p => p.ProductPrice <= maxPrice.Value);

            if (startDate != null && startDate.HasValue)
                query = query.Where(p => p.CreatedAt >= startDate.Value);

            if (endDate != null && endDate.HasValue)
                query = query.Where(p => p.CreatedAt <= endDate.Value);

            return await query.ToListAsync();
        }
        public async Task Create(ProductDetails product)
        {
            if (product.ProductPrice > 10000)
                throw new InvalidOperationException("Product price cannot exceed $10000");
            if(product.ProductPrice > 5000)
            {
                var approvalReq = new ApprovalRequest
                {
                    ProductId = product.Id,
                    RequestReason = "Price is over 5000 and under 10000",
                    RequestType = "Create",
                    RequestDate = DateTime.Now
                };
                _unitOfWork.Products.AddToApprovalQueue(approvalReq);
            }
            await _unitOfWork.Products.Add(product);
            await _unitOfWork.Save();
        }
        public async Task Update (ProductDetails product)
        {
            var existingProduct = await _unitOfWork.Products.GetById(product.Id);
            if (existingProduct == null)
                throw new ArgumentException("Product not found");
            if (product.ProductPrice > 5000 || product.ProductPrice > existingProduct.ProductPrice * 1.5)
            {
                var approvalReq = new ApprovalRequest
                {
                    ProductId = product.Id,
                    RequestReason = product.ProductPrice > 5000 ? "Price is over 5000 and under 10000" : "Price increased by 50%",
                    RequestType = "Update",
                    RequestDate = DateTime.Now
                };
                _unitOfWork.Products.AddToApprovalQueue(approvalReq);
            }
            await _unitOfWork.Products.Add(product);
            await _unitOfWork.Save();
        }
        public async Task Delete (int productId)
        {
            var product = _unitOfWork.Products.GetById(productId);
            if (product == null)
                throw new ArgumentException("Product not found");

            var approval = new ApprovalRequest
            {
                ProductId = productId,
                RequestReason = "Product deletion requested",
                RequestType = "Delete",
                RequestDate = DateTime.Now
            };
            _unitOfWork.Products.AddToApprovalQueue(approval);
        }
    }
}
