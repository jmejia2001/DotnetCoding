using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductDetails>
    {
        IEnumerable<ProductDetails> GetActiveProducts(string name, decimal? minPrice, decimal? maxPrice, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<ApprovalRequest>> GetApprovalQueue();
        void AddToApprovalQueue(ApprovalRequest approval);
    }
}
