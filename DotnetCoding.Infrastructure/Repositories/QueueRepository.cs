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
        public async Task Approve(int id)
        {
            var approvalRequest = await _context.ApprovalQueue.FindAsync(id);
            if (approvalRequest != null)
            {
                // Approve logic
                approvalRequest.Product.ProductStatus = "Approved";
                _context.ApprovalQueue.Remove(approvalRequest);
                await _context.SaveChangesAsync();
            }
        }
        public async Task RejectAsync(int id)
        {
            var approvalRequest = await _context.ApprovalQueue.FindAsync(id);
            if (approvalRequest != null)
            {
                // Reject logic
                approvalRequest.Product.ProductStatus = "Rejected";
                _context.ApprovalQueue.Remove(approvalRequest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
