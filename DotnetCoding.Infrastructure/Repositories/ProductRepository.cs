using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductDetails>, IProductRepository
    {
        public DbContextClass _context;
        public ProductRepository(DbContextClass dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
        public IEnumerable<ProductDetails> GetActiveProducts(string name, decimal? minPrice, decimal? maxPrice, DateTime? startDate, DateTime? endDate)
        {
            var query = (IQueryable<ProductDetails>)_context.Products.Where(p => p.Active).OrderByDescending(p => p.CreatedAt);
            
            if(!string.IsNullOrEmpty(name))
                    query = query.Where(p => p.ProductName.Contains(name));

            if (minPrice.HasValue)
                query = query.Where(p => p.ProductPrice >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.ProductPrice <= maxPrice.Value);

            if (startDate.HasValue)
                query = query.Where(p => p.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(p => p.CreatedAt <= endDate.Value);

            return query.ToList();
        }
        public async Task<IEnumerable<ApprovalRequest>> GetApprovalQueue()
        {
            return await _context.ApprovalQueue.OrderBy(q => q.RequestDate).Include(q => q.Product).ToListAsync();
        }
        public void AddToApprovalQueue(ApprovalRequest approval)
        {
            _context.ApprovalQueue.Add(approval);
            _context.SaveChanges();
        }
    }
}
