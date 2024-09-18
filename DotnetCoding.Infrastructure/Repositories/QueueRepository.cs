using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class QueueRepository : GenericRepository<ProductDetails>, IQueueRepository
    {
        private readonly DbContextClass _context;
        public QueueRepository(DbContextClass dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<ApprovalRequest>> GetApprovalQueue()
        {
            return await _context.ApprovalQueue.OrderBy(q => q.RequestDate).Include(q => q.Product).ToListAsync();
        }
    }
}
