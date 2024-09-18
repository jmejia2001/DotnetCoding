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

        public void AddToApprovalQueue(ApprovalRequest approval)
        {
            _context.ApprovalQueue.Add(approval);
            _context.SaveChanges();
        }
    }
}
