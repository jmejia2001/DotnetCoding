

namespace DotnetCoding.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IQueueRepository ApprovalQueues { get; }

        Task Save();
    }
}
