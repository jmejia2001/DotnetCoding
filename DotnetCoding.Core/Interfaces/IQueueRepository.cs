using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IQueueRepository : IGenericRepository<ProductDetails>
    {

        Task<IEnumerable<ApprovalRequest>> GetApprovalQueue();
    }
}
