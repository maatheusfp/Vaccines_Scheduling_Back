using System.Data;

namespace Vaccines_Scheduling.Repository.Interface
{
    public interface IMandatoryTransaction
    {
        Task BeginTransactionAsync(IsolationLevel isolationLevel);
        Task CommitTransactionsAsync();
        Task RollbackTransactionsAsync();
    }
}
