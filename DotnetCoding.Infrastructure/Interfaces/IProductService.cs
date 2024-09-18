using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Infrastructure.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetails>> GetAllProducts();
        Task<IEnumerable<ApprovalRequest>> GetApprovalQueue();
        Task<IEnumerable<ProductDetails>> GetActiveProducts(string name, decimal? minPrice, decimal? maxPrice, DateTime? startDate, DateTime? endDate);
        Task Create(ProductDetails product);
        Task Update(ProductDetails product);
        Task Delete(int product);
    }
}
